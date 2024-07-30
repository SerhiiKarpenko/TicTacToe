using Code_Base.Enums;
using Code_Base.Infrastructure.Configs.AssetPaths;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.GridFactory;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Code_Base.Infrastructure.Services.GridSignFactory;
using Code_Base.Infrastructure.Services.HintService;
using Code_Base.Infrastructure.Services.LoadingCurtainFactory;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.Infrastructure.Services.SceneLoader;
using Code_Base.Infrastructure.Services.StaticDataService;
using Code_Base.Infrastructure.Services.TimerPresenterProvider;
using Code_Base.Infrastructure.Services.UIFactory;
using Code_Base.Infrastructure.StaticData;
using Code_Base.TicTacToeGrid;
using Code_Base.UI.Curtain;
using Code_Base.UI.Decorataion;
using Code_Base.UI.MainHud;
using Code_Base.UI.Timer;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameStateMachine.States
{
  public class LoadMainGameSceneState : IPayloadState<string>
  {
    private IGridPresenter gridPresenter;
    private IRoundTimerPresenter roundTimerPresenter;
    private IDecorationBackgroundPresenter decorationBackgroundPresenter;
    private ILoadingCurtain sceneLoadingCurtain;
    
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IUIFactory uiFactory;
    private readonly IGridFactory gridFactory;
    private readonly IStaticDataService staticDataService;
    private readonly ILoadingCurtainFactory loadingCurtainFactory;
    private readonly IGridPresenterProvider gridPresenterProvider;
    private readonly IGameSetupService gameSetupService;
    private readonly IHintService hintService;
    private readonly ICommandService commandService;
    private readonly IPlayersOrderService playersOrderService;
    private readonly IAssetsFromBundleProviderService assetsFromBundleProviderService;
    private readonly IRoundTimerPresenterProvider roundTimerPresenterProvider;
    private readonly IAssetBundleLoadService assetBundleLoadService;

    private readonly AssetBundleAssetsPaths assetBundleAssetsPaths;

    [Inject]
    public LoadMainGameSceneState(
      IGameStateMachine gameStateMachine,
      ISceneLoader sceneLoader,
      IUIFactory uiFactory,
      IStaticDataService staticDataService, 
      IGridFactory gridFactory, 
      ILoadingCurtainFactory loadingCurtainFactory,
      IGridPresenterProvider gridPresenterProvider,
      IGridSignFactory gridSignFactory, 
      IGameSetupService gameSetupService, 
      IHintService hintService, 
      ICommandService commandService, 
      IPlayersOrderService playersOrderService, 
      IAssetsFromBundleProviderService assetsFromBundleProviderService,
      IAssetBundleLoadService assetBundleLoadService,
      IRoundTimerPresenterProvider roundTimerPresenterProvider,
      AssetBundleAssetsPaths assetBundleAssetsPaths)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.uiFactory = uiFactory;
      this.staticDataService = staticDataService;
      this.gridFactory = gridFactory;
      this.loadingCurtainFactory = loadingCurtainFactory;
      this.gridPresenterProvider = gridPresenterProvider;
      this.gameSetupService = gameSetupService;
      this.hintService = hintService;
      this.commandService = commandService;
      this.playersOrderService = playersOrderService;
      this.assetsFromBundleProviderService = assetsFromBundleProviderService;
      this.assetBundleLoadService = assetBundleLoadService;
      this.roundTimerPresenterProvider = roundTimerPresenterProvider;
      this.assetBundleAssetsPaths = assetBundleAssetsPaths;
    }
    
    public void Enter(string levelName)
    {
      assetsFromBundleProviderService.Cleanup();
      
      sceneLoadingCurtain = loadingCurtainFactory.CreateSceneLoadingCurtain();
      sceneLoadingCurtain.Show();
      
      sceneLoader.Load(levelName, OnLoaded);
    }

    public void Exit()
    {
      sceneLoadingCurtain.Hide();
      decorationBackgroundPresenter.Cleanup();
    }

    private void OnLoaded()
    {
      InitGameWorld();
    }

    private void InitGameWorld()
    {
      LevelStaticData levelStaticData = ForLevel(SceneManager.GetActiveScene().name);
      
      uiFactory.CreateUIRoot();
      InitializeBackgroundVisual();

      if (gameSetupService.GameMode == GameMode.PlayerVsComputer)
      {
        InitializeMainHud();
        CreateRoundTimer();
        
        roundTimerPresenterProvider.SetupTimerPresenter(roundTimerPresenter);
      }
      
      BuildGrid(levelStaticData);
      InitializeGrid();
      
      gridPresenterProvider.SetupGridPresenter(gridPresenter);
      
      gameStateMachine.Enter<GameLoopInitialState>();
    }

    private void InitializeBackgroundVisual()
    {
      DecorationBackgroundVisual decorationBackgroundVisual = uiFactory.CreateDecorationBackground();

      decorationBackgroundPresenter = new DecorationBackgroundPresenter(
        decorationBackgroundVisual,
        assetBundleLoadService,
        assetBundleAssetsPaths,
        assetsFromBundleProviderService
      );

      decorationBackgroundPresenter.Initialize();
    }

    private void InitializeMainHud()
    {
      MainHudVisual mainHudVisual = uiFactory.CreateMainHud();
      
      IMainHudPresenter mainHudPresenter = new MainHudPresenter(
        mainHudVisual,
        hintService,
        gameStateMachine,
        commandService,
        playersOrderService,
        gameSetupService
      );
      
      mainHudPresenter.Initialize();
    }

    private void CreateRoundTimer()
    {
      RoundTimerVisual roundTimerVisual = uiFactory.CreateTimerVisual();
      roundTimerPresenter = new RoundTimerPresenter(roundTimerVisual);
    }

    private void BuildGrid(LevelStaticData levelStaticData)
    {
      IGrid grid = gridFactory.CreateGrid(levelStaticData.GridWidht, levelStaticData.GridHeight);
      IGridVisual gridVisual = uiFactory.CreateGridPanel();
      gridPresenter = gridFactory.CreateGridPresenter(grid, gridVisual);
    }

    private void InitializeGrid()
    {
      gridPresenter.InitializeGrid();
      gridPresenter.InitializeGridVisual();
      gridPresenter.InitializeCellsPresenters();
    }

    private LevelStaticData ForLevel(string sceneKey) => 
      staticDataService.ForLevel(sceneKey);
  }
}