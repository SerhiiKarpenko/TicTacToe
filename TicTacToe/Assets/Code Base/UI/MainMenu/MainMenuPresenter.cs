using System;
using Code_Base.Enums;
using Code_Base.Infrastructure.Configs.SceneNames;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine;
using Code_Base.Infrastructure.Services.GameStateMachine.States;
using UnityEngine.UI;

namespace Code_Base.UI.MainMenu
{
  public class MainMenuPresenter : IMainMenuPresenter
  {
    private readonly MainMenu mainMenu;
    private readonly MainMenuVisual mainMenuVisual;
    private readonly SceneNames sceneNames;
    private readonly IGameSetupService gameSetupService;
    private readonly IGameStateMachine gameStateMachine;
    private readonly IAssetBundleLoadService assetBundleLoadService;
    
    public MainMenuPresenter(
      MainMenuVisual mainMenuVisual,
      MainMenu mainMenu,
      SceneNames sceneNames, 
      IGameSetupService gameSetupService,
      IGameStateMachine gameStateMachine, 
      IAssetBundleLoadService assetBundleLoadService)
    {
      this.mainMenuVisual = mainMenuVisual;
      this.mainMenu = mainMenu;
      this.gameSetupService = gameSetupService;
      this.gameStateMachine = gameStateMachine;
      this.assetBundleLoadService = assetBundleLoadService;
      this.sceneNames = sceneNames;
    }

    public void Initialize()
    {
      SubscribeOnTogglesValueChange();
      SubscribeOnStartButtonClick();
      SubscribeOnReskinButtonClick();
      InitializeVisualToggles();
      InitializeVisualInputField();
    }

    private void SubscribeOnTogglesValueChange()
    {
      mainMenuVisual.PlayerVsPlayer.onValueChanged.AddListener(OnPlayerVsPlayerToggled);
      mainMenuVisual.PlayerVsComputer.onValueChanged.AddListener(OnPlayerVsComputerToggled);
      mainMenuVisual.ComputerVsComputer.onValueChanged.AddListener(OnComputerVsComputerToggled);
    }

    private void SubscribeOnStartButtonClick()
    {
      mainMenuVisual.Start.onClick.AddListener(OnStartButtonClick);
    }

    private void SubscribeOnReskinButtonClick()
    {
      mainMenuVisual.Reskin.onClick.AddListener(OnReskinButtonClick);
    }

    private void InitializeVisualToggles()
    {
      switch (mainMenu.GameMode)
      {
        case GameMode.PlayerVsPlayer:
          mainMenuVisual.PlayerVsPlayer.isOn = true;
          break;
        case GameMode.PlayerVsComputer:
          mainMenuVisual.PlayerVsComputer.isOn = true;
          break;
        case GameMode.ComputerVsComputer:
          mainMenuVisual.ComputerVsComputer.isOn = true;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(mainMenu.GameMode), mainMenu.GameMode, null);
      }
    }

    private void InitializeVisualInputField() => 
      mainMenuVisual.BundleNameInputField.text = assetBundleLoadService.CurrentAssetBundle.name;

    private void OnPlayerVsPlayerToggled(bool toggled)
    {
      OnToggleChanged(
        toggled,
        GameMode.PlayerVsPlayer,
        toggleToBlock: mainMenuVisual.PlayerVsPlayer,
        new Toggle[] { mainMenuVisual.PlayerVsComputer, mainMenuVisual.ComputerVsComputer }
        );
    }

    private void OnPlayerVsComputerToggled(bool toggled)
    {
      OnToggleChanged(
        toggled,
        GameMode.PlayerVsComputer,
        toggleToBlock: mainMenuVisual.PlayerVsComputer,
        new Toggle[] { mainMenuVisual.PlayerVsPlayer, mainMenuVisual.ComputerVsComputer }
      );
    }

    private void OnComputerVsComputerToggled(bool toggled)
    {
      OnToggleChanged(
        toggled,
        GameMode.ComputerVsComputer,
        toggleToBlock: mainMenuVisual.ComputerVsComputer,
        new Toggle[] { mainMenuVisual.PlayerVsPlayer, mainMenuVisual.PlayerVsComputer }
      );
    }

    private void OnToggleChanged(bool toggled, GameMode gameMode, Toggle toggleToBlock, Toggle[] togglesToDeactivate)
    {
      if (toggled)
      {
        foreach (Toggle toggle in togglesToDeactivate)
        {
          DeactivateToggle(toggle);
        }

        BlockToggle(toggleToBlock);
        SetGameMode(gameMode);
      }
    }

    private void SetGameMode(GameMode gameMode) => 
      mainMenu.GameMode = gameMode;

    private void DeactivateToggle(Toggle inactiveToggle)
    {
      inactiveToggle.interactable = true;
      inactiveToggle.isOn = false;
    }

    private void BlockToggle(Toggle activeToggle) => 
      activeToggle.interactable = false;

    private void OnStartButtonClick()
    {
      gameSetupService.SetupGameMode(mainMenu.GameMode);
      gameStateMachine.Enter<LoadMainGameSceneState, string>(sceneNames.MainGameSceneName);
    }

    private void OnReskinButtonClick() => 
      assetBundleLoadService.LoadAssetBundle(mainMenuVisual.BundleNameInputField.text.ToLower());
  }
}