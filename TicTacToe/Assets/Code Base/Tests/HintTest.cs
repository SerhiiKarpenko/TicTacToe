using Code_Base.Infrastructure.Configs.AssetPaths;
using Code_Base.Infrastructure.Configs.ResourceAssetPaths;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using Code_Base.Infrastructure.Services.AssetsFromResourcesProvider;
using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.GridFactory;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Code_Base.Infrastructure.Services.GridSignFactory;
using Code_Base.Infrastructure.Services.HintService;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.Infrastructure.Services.UIFactory;
using Code_Base.TicTacToeGrid;
using NUnit.Framework;
using Zenject;

namespace Code_Base.Tests
{
  public class HintTestInstaller : Installer<HintTestInstaller>
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
      Container.BindInterfacesTo<HintService>().AsSingle();
      Container.BindInterfacesTo<GridFactory>().AsSingle();
      Container.BindInterfacesTo<GridSignFactory>().AsSingle();
      Container.BindInterfacesTo<UIFactory>().AsSingle();
      Container.BindInterfacesTo<ResourcesAssetProviderService>().AsSingle();
      Container.BindInterfacesTo<PlayersOrderService>().AsSingle();
      Container.BindInterfacesTo<GridPresenterProvider>().AsSingle();
      Container.BindInterfacesTo<AssetsFromBundleProviderService>().AsSingle();
      Container.BindInterfacesTo<AssetBundleLoadService>().AsSingle();
    }
  }
  
  [TestFixture]
  public class HintTest : ZenjectUnitTestFixture
  {
    
    [SetUp]
    public void BindInterfaces()
    {
      HintTestInstaller.Install(Container);
    }
    
    [Test]
    public void TestHint()
    {
      IHintService hintService = Container.Resolve<IHintService>();
      Assert.NotNull(hintService, "hintService == null");
      
      IUIFactory uiFactory = Container.Resolve<IUIFactory>();
      Assert.NotNull(uiFactory, "uiFactory == null");
      
      IGridFactory gridFactory = Container.Resolve<IGridFactory>();
      Assert.NotNull(gridFactory, "gridFactory == null");
      
      IGridPresenterProvider gridPresenterProvider = Container.Resolve<IGridPresenterProvider>();
      Assert.NotNull(gridPresenterProvider, "gridPresenterProvider == null");
      
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
      
      hintService.ShowHint();
    }
    
    [TearDown]
    public void TearDown()
    {
      Container.UnbindAll();
    }
  }
}