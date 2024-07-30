using Code_Base.Infrastructure.Configs.AssetPaths;
using Code_Base.Infrastructure.Configs.InfrastructureResourcesPaths;
using Code_Base.Infrastructure.Configs.InitialGameConfig;
using Code_Base.Infrastructure.Configs.ResourceAssetPaths;
using Code_Base.Infrastructure.Configs.SceneNames;
using Code_Base.Infrastructure.Configs.StaticDataPaths;
using Code_Base.Infrastructure.CoroutineRunner;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using Code_Base.Infrastructure.Services.AssetsFromResourcesProvider;
using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.DrawWinService;
using Code_Base.Infrastructure.Services.GameBootstrapperFactory;
using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine;
using Code_Base.Infrastructure.Services.GridFactory;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Code_Base.Infrastructure.Services.GridSignFactory;
using Code_Base.Infrastructure.Services.HintService;
using Code_Base.Infrastructure.Services.LoadingCurtainFactory;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.Infrastructure.Services.PlayerFactory;
using Code_Base.Infrastructure.Services.RandomService;
using Code_Base.Infrastructure.Services.SceneLoader;
using Code_Base.Infrastructure.Services.StateFactory;
using Code_Base.Infrastructure.Services.StaticDataService;
using Code_Base.Infrastructure.Services.TimerPresenterProvider;
using Code_Base.Infrastructure.Services.TimerService;
using Code_Base.Infrastructure.Services.TimeService;
using Code_Base.Infrastructure.Services.UIFactory;
using UnityEngine;
using Zenject;

namespace Code_Base.CompositionRoot
{
  public class GameInstaller : MonoInstaller
  {
    [SerializeField] private InfrastructureResourcesPaths infrastructureResourcesPaths;
    [SerializeField] private StaticDataPaths staticDataPaths;
    [SerializeField] private AssetBundleAssetsPaths assetBundleAssetsPaths;
    [SerializeField] private ResourceAssetsPaths resourceAssetsPaths;
    [SerializeField] private InitialGameConfig initialGameConfig;
    [SerializeField] private SceneNames sceneNames;

    public override void InstallBindings()
    {
      InstallConfigs();
      InstallServices();
    }

    private void InstallConfigs()
    {
      BindInfrastructurePaths();
      BindStaticDataPaths();
      BindInitialGameConfig();
      BindSceneNamesConfig();
      BindAssetBundleAssetPaths();
      BindResourceAssetsPaths();
    }

    private void InstallServices()
    {
      BindGameStateMachine();
      
      BindGameStateFactory();
      BindBootstrapperFactory();
      BindLoadingCurtainFactory();
      BindUIFactory();
      BindGridFactory();
      BindSignFactory();
      BindPlayerFactory();
      
      BindCoroutineRunner();
      BindSceneLoader();
      
      BindTimeService();
      BindRandomService();
      BindStaticDataService();
      BindGameSetupService();
      BindTimerService();
      BindNextPlayerService();
      BindGridPresenterProvider();
      BindDrawWinService();
      BindHintService();
      BindCommandService();
      BindAssetBundleLoadService();
      BindAssetsFromBundleProviderService();
      BindResourceAssetProviderSerivce();
      BindRoundTimerPresenterProvider();
    }

    private void BindInfrastructurePaths() =>
      Container
        .BindInstance(infrastructureResourcesPaths)
        .AsSingle();

    private void BindStaticDataPaths() =>
      Container
        .BindInstance(staticDataPaths)
        .AsSingle();

    private void BindInitialGameConfig() =>
      Container
        .BindInstance(initialGameConfig)
        .AsSingle();

    private void BindSceneNamesConfig() =>
      Container
        .BindInstance(sceneNames)
        .AsSingle();

    private void BindAssetBundleAssetPaths() =>
      Container
        .BindInstance(assetBundleAssetsPaths)
        .AsSingle();

    private void BindResourceAssetsPaths() =>
      Container
        .BindInstance(resourceAssetsPaths)
        .AsSingle();


    private void BindGameStateMachine() =>
      Container
        .BindInterfacesTo<GameStateMachine>()
        .AsSingle();

    private void BindGameStateFactory() =>
      Container
        .BindInterfacesTo<StateFactory>()
        .AsSingle();

    private void BindSignFactory() =>
      Container
        .BindInterfacesTo<GridSignFactory>()
        .AsSingle();

    private void BindPlayerFactory() =>
      Container
        .BindInterfacesTo<PlayerFactory>()
        .AsSingle();

    private void BindBootstrapperFactory() =>
      Container
        .BindInterfacesTo<GameBootstrapperFactory>()
        .AsSingle();

    private void BindLoadingCurtainFactory() =>
      Container
        .BindInterfacesTo<LoadingCurtainFactory>()
        .AsSingle();

    private void BindUIFactory() =>
      Container
        .BindInterfacesTo<UIFactory>()
        .AsSingle();

    private void BindGridFactory() =>
      Container
        .BindInterfacesTo<GridFactory>()
        .AsSingle();


    private void BindCoroutineRunner() =>
      Container
        .Bind<ICoroutineRunner>()
        .To<CoroutineRunner>()
        .FromComponentInNewPrefabResource(infrastructureResourcesPaths.CoroutineRunnerPath)
        .AsSingle();

    private void BindSceneLoader() =>
      Container
        .BindInterfacesTo<SceneLoader>()
        .AsSingle();


    private void BindTimeService() =>
      Container
        .BindInterfacesTo<TimeService>()
        .AsSingle();

    private void BindRandomService() =>
      Container
        .BindInterfacesTo<RandomService>()
        .AsSingle();

    private void BindStaticDataService() =>
      Container
        .BindInterfacesTo<StaticDataService>()
        .AsSingle()
        .WithArguments(staticDataPaths);

    private void BindGameSetupService() =>
      Container
        .BindInterfacesTo<GameSetupService>()
        .AsSingle();

    private void BindTimerService() =>
      Container
        .BindInterfacesTo<TimerService>()
        .AsSingle();

    private void BindNextPlayerService() =>
      Container
        .BindInterfacesTo<PlayersOrderService>()
        .AsSingle();

    private void BindGridPresenterProvider() =>
      Container
        .BindInterfacesTo<GridPresenterProvider>()
        .AsSingle();

    private void BindDrawWinService() =>
      Container
        .BindInterfacesTo<DrawWinService>()
        .AsSingle();

    private void BindHintService() =>
      Container
        .BindInterfacesTo<HintService>()
        .AsSingle();

    private void BindCommandService() =>
      Container
        .BindInterfacesTo<CommandService>()
        .AsSingle();

    private void BindAssetBundleLoadService() =>
      Container
        .BindInterfacesTo<AssetBundleLoadService>()
        .AsSingle();

    private void BindAssetsFromBundleProviderService() =>
      Container
        .BindInterfacesTo<AssetsFromBundleProviderService>()
        .AsSingle();

    private void BindResourceAssetProviderSerivce() =>
      Container
        .BindInterfacesTo<ResourcesAssetProviderService>()
        .AsSingle();

    private void BindRoundTimerPresenterProvider() =>
      Container
        .BindInterfacesTo<RoundRoundTimerPresenterProvider>()
        .AsSingle();
  }
  
}
