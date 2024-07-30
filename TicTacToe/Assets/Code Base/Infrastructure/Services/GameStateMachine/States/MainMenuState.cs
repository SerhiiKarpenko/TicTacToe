using Code_Base.Infrastructure.Configs.AssetPaths;
using Code_Base.Infrastructure.Configs.InitialGameConfig;
using Code_Base.Infrastructure.Configs.SceneNames;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.LoadingCurtainFactory;
using Code_Base.Infrastructure.Services.SceneLoader;
using Code_Base.Infrastructure.Services.UIFactory;
using Code_Base.UI.Curtain;
using Code_Base.UI.Decorataion;
using Code_Base.UI.MainMenu;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameStateMachine.States
{
  public class MainMenuState : IState
  {
    private ILoadingCurtain sceneLoadingCurtain;
    private IMainMenuPresenter mainMenuPresenter;
    private IDecorationBackgroundPresenter backgroundPresenter;

    private readonly IGameStateMachine gameStateMachine;
    private readonly IUIFactory uiFactory;
    private readonly ISceneLoader sceneLoader;
    private readonly ILoadingCurtainFactory loadingCurtainFactory;
    private readonly IGameSetupService gameSetupService;
    private readonly IAssetBundleLoadService assetBundleLoadService;
    private readonly IAssetsFromBundleProviderService assetsFromBundleProviderService;

    private readonly InitialGameConfig initialGameConfig;
    private readonly SceneNames sceneNames;
    private readonly AssetBundleAssetsPaths assetBundleAssetsPaths;


    [Inject]
    public MainMenuState(
      IGameStateMachine gameStateMachine,
      IUIFactory uiFactory,
      ISceneLoader sceneLoader,
      ILoadingCurtainFactory loadingCurtainFactory,
      IGameSetupService gameSetupService,
      IAssetBundleLoadService assetBundleLoadService,
      IAssetsFromBundleProviderService assetsFromBundleProviderService, 
      InitialGameConfig initialGameConfig,
      SceneNames sceneNames,
      AssetBundleAssetsPaths assetBundleAssetsPaths)
    {
      this.gameStateMachine = gameStateMachine;
      this.uiFactory = uiFactory;
      this.sceneLoader = sceneLoader;
      this.loadingCurtainFactory = loadingCurtainFactory;
      this.assetsFromBundleProviderService = assetsFromBundleProviderService;
      this.assetBundleLoadService = assetBundleLoadService;
      this.gameSetupService = gameSetupService;
      this.initialGameConfig = initialGameConfig;
      this.sceneNames = sceneNames;
      this.assetBundleAssetsPaths = assetBundleAssetsPaths;
    }
    
    public void Enter()
    {
      sceneLoadingCurtain = loadingCurtainFactory.CreateSceneLoadingCurtain();
      sceneLoadingCurtain.Show();
      
      sceneLoader.Load(sceneNames.MainMenuSceneName, InitializeMainMenu);
    }

    public void Exit() => 
      backgroundPresenter.Cleanup();

    private void InitializeMainMenu()
    {
      gameSetupService.SetupGameMode(initialGameConfig.GameMode);
      
      uiFactory.CreateUIRoot();
      CreateDecorationBackground();
      CreateMainMenu();

      sceneLoadingCurtain.Hide();
    }

    private void CreateMainMenu()
    {
      MainMenu mainMenu = new MainMenu(initialGameConfig.GameMode);
      MainMenuVisual mainMenuVisual = uiFactory.CreateMainMenu();

      mainMenuPresenter = new MainMenuPresenter(
        mainMenuVisual,
        mainMenu,
        sceneNames,
        gameSetupService,
        gameStateMachine,
        assetBundleLoadService
      );

      mainMenuPresenter.Initialize();
    }

    private void CreateDecorationBackground()
    {
      DecorationBackgroundVisual decorationBackgroundVisual = uiFactory.CreateDecorationBackground();

      backgroundPresenter = new DecorationBackgroundPresenter(
        decorationBackgroundVisual,
        assetBundleLoadService,
        assetBundleAssetsPaths,
        assetsFromBundleProviderService
      );

      backgroundPresenter.Initialize();
    }
  }
}