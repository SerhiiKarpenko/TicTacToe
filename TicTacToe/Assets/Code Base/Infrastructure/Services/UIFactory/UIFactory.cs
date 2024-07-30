using Code_Base.Infrastructure.Configs.ResourceAssetPaths;
using Code_Base.Infrastructure.Services.AssetsFromResourcesProvider;
using Code_Base.TicTacToeGrid;
using Code_Base.UI.Decorataion;
using Code_Base.UI.EndGameWindow;
using Code_Base.UI.MainHud;
using Code_Base.UI.MainMenu;
using Code_Base.UI.Timer;
using UnityEngine;
using Zenject;

namespace Code_Base.Infrastructure.Services.UIFactory
{
  public class UIFactory : IUIFactory
  {
    private GameObject uiRoot;
    private GridVisual gridVisual;
    
    private readonly IResourcesAssetProviderService resourcesAssetProviderService;
    private readonly ResourceAssetsPaths resourceAssetsPaths;
    private readonly DiContainer diContainer;

    [Inject]
    public UIFactory(
      IResourcesAssetProviderService resourcesAssetProviderService,
      ResourceAssetsPaths resourceAssetsPaths,
      DiContainer diContainer
      )
    {
      this.resourcesAssetProviderService = resourcesAssetProviderService;
      this.resourceAssetsPaths = resourceAssetsPaths;
      this.diContainer = diContainer;
    }
    
    public void CreateUIRoot()
    {
      GameObject loadedUiRoot = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.UIRootPath);
      uiRoot = Object.Instantiate(loadedUiRoot);
    }

    public GridVisual CreateGridPanel()
    {
      GameObject gridPanel = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.GridPanelPath);
      gridVisual = diContainer.InstantiatePrefabForComponent<GridVisual>(gridPanel, uiRoot.transform);
      
      return gridVisual;
    }

    public GridCellVisual CreateGirdCellVisual()
    {
      GameObject gridCellVisualPrefab = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.GridCellVisualPath);
      GameObject gridCellVisualClone = diContainer.InstantiatePrefab(gridCellVisualPrefab, gridVisual.transform);
      GridCellVisual girdCellVisual = gridCellVisualClone.GetComponent<GridCellVisual>();
      
      return girdCellVisual;
    }

    public MainMenuVisual CreateMainMenu()
    {
      GameObject mainMenuPrefab = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.MainMenuVisualPath);
      GameObject mainMenuClone = Object.Instantiate(mainMenuPrefab, uiRoot.transform);
      MainMenuVisual mainMenuVisual = mainMenuClone.GetComponent<MainMenuVisual>();
      
      return mainMenuVisual;
    }

    public EndGameVisual CreateEndGameMenu()
    {
      GameObject endGamePrefab = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.EndGameVisualPath);
      GameObject endGameClone = Object.Instantiate(endGamePrefab, uiRoot.transform);
      EndGameVisual endGameVisual = endGameClone.GetComponent<EndGameVisual>();

      return endGameVisual;
    }

    public MainHudVisual CreateMainHud()
    {
      GameObject mainHudPrefab = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.MainHudVisualPath);
      GameObject mainHudClone = Object.Instantiate(mainHudPrefab, uiRoot.transform);
      MainHudVisual mainHudVisual = mainHudClone.GetComponent<MainHudVisual>();

      return mainHudVisual;
    }

    public DecorationBackgroundVisual CreateDecorationBackground()
    {
      GameObject mainHudPrefab = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.DecorationVisualPath);
      GameObject mainHudClone = Object.Instantiate(mainHudPrefab, uiRoot.transform);
      DecorationBackgroundVisual mainHudBackgroundVisual = mainHudClone.GetComponent<DecorationBackgroundVisual>();

      return mainHudBackgroundVisual;
    }

    public RoundTimerVisual CreateTimerVisual()
    {
      GameObject timerVisualPrefab = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.TimerVisualPath);
      GameObject timerVisualClone = Object.Instantiate(timerVisualPrefab, uiRoot.transform);
      RoundTimerVisual roundTimerVisual = timerVisualClone.GetComponent<RoundTimerVisual>();

      return roundTimerVisual;
    }
  }
}