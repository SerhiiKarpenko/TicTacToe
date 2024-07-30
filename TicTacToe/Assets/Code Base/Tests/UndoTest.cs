using Code_Base.Enums;
using Code_Base.Infrastructure.Configs.AssetPaths;
using Code_Base.Infrastructure.Configs.ResourceAssetPaths;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using Code_Base.Infrastructure.Services.AssetsFromResourcesProvider;
using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.GridFactory;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Code_Base.Infrastructure.Services.GridSignFactory;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.Infrastructure.Services.TimerService;
using Code_Base.Infrastructure.Services.TimeService;
using Code_Base.Infrastructure.Services.UIFactory;
using Code_Base.Player;
using Code_Base.TicTacToeGrid;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Code_Base.Tests
{
  
  public class UndoTestInstaller : Installer<UndoTestInstaller>
  {
    private string resourceAssetsPath = "UnitTests/UnitTestingResourceAssetsPaths";
    private string assetBundleAssetsPath = "UnitTests/UnitTestingAssetBundleAssetsNames";

    public override void InstallBindings()
    {
      ResourceAssetsPaths resourceAssetsPaths = 
        Container.InstantiateScriptableObjectResource(typeof (ResourceAssetsPaths), resourceAssetsPath) as ResourceAssetsPaths;
      
      AssetBundleAssetsPaths assetBundleAssetsPaths = 
        Container.InstantiateScriptableObjectResource(typeof (AssetBundleAssetsPaths), assetBundleAssetsPath) as AssetBundleAssetsPaths;
      
      Container.BindInstance(resourceAssetsPaths).AsSingle();
      Container.BindInstance(assetBundleAssetsPaths).AsSingle();
      
      Container.BindInterfacesTo<CommandService>().AsSingle();
      Container.BindInterfacesTo<GridFactory>().AsSingle();
      Container.BindInterfacesTo<UIFactory>().AsSingle();
      Container.BindInterfacesTo<GridSignFactory>().AsSingle();
      Container.BindInterfacesTo<ResourcesAssetProviderService>().AsSingle();
      Container.BindInterfacesTo<PlayersOrderService>().AsSingle();
      Container.BindInterfacesTo<GridPresenterProvider>().AsSingle();
      Container.BindInterfacesTo<AssetsFromBundleProviderService>().AsSingle();
      Container.BindInterfacesTo<AssetBundleLoadService>().AsSingle();
      Container.BindInterfacesTo<TimerService>().AsSingle();
      Container.BindInterfacesTo<TimeService>().AsSingle();
    }
  }
  
  [TestFixture]
  public class UndoTest : ZenjectUnitTestFixture
  {
    [SetUp]
    public void BindInterfaces()
    {
      UndoTestInstaller.Install(Container);
    }
    
    [Test]
    public void TestUndo()
    {
      ICommandService commandService = Container.Resolve<ICommandService>();
      Assert.NotNull(commandService, "hintService == null");
      
      IUIFactory uiFactory = Container.Resolve<IUIFactory>();
      Assert.NotNull(uiFactory, "uiFactory == null");
      
      IGridFactory gridFactory = Container.Resolve<IGridFactory>();
      Assert.NotNull(gridFactory, "gridFactory == null");
      
      IGridPresenterProvider gridPresenterProvider = Container.Resolve<IGridPresenterProvider>();
      Assert.NotNull(gridPresenterProvider, "gridPresenterProvider == null");
      
      IPlayersOrderService playersOrderService = Container.Resolve<IPlayersOrderService>();
      Assert.NotNull(playersOrderService, "playersOrderService != null");

      ITimerService timerService = Container.Resolve<ITimerService>();
      Assert.NotNull(timerService, "timerService != null");

      IAssetBundleLoadService assetBundleLoadService = Container.Resolve<IAssetBundleLoadService>();
      Assert.NotNull(assetBundleLoadService, "assetBundleLoadService != null");
      
      assetBundleLoadService.LoadAssetBundle("defaultbundle");
      
      IGrid grid = gridFactory.CreateGrid(3 ,3);
      Assert.NotNull(grid, "grid != null");
      
      uiFactory.CreateUIRoot();
      IGridVisual gridVisual = uiFactory.CreateGridPanel();
      
      IGridPresenter gridPresenter = gridFactory.CreateGridPresenter(grid, gridVisual);
      Assert.NotNull(gridPresenter, "gridPresenter != null");
      
      gridPresenter.InitializeGrid();
      gridPresenter.InitializeGridVisual();
      gridPresenter.InitializeCellsPresenters();
      gridPresenterProvider.SetupGridPresenter(gridPresenter);
      gridPresenter.ClearGrid();
      
      Assert.IsNotEmpty(grid.GridCells, "grid cells is not empty");

      IPlayer player = new ComputerPlayer(SignType.Circle, PlayerType.FirstPlayer, gridPresenter, 0, timerService);
      playersOrderService.SetCurrentPlayer(player);
      
      gridPresenterProvider.GridPresenter.GetRandomNonOccupiedGridCellPresenter().MakeMoveCommand();
      commandService.UndoLastMove();
    }
    
    [TearDown]
    public void TearDown()
    {
      AssetBundle.UnloadAllAssetBundles(true);
      Container.UnbindAll();
    }
  }
}