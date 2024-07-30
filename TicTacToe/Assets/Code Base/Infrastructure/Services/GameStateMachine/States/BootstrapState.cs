using Code_Base.Infrastructure.Configs.InitialGameConfig;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.StaticDataService;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameStateMachine.States
{
  public class BootstrapState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly IStaticDataService staticDataService;
    private readonly IAssetBundleLoadService assetBundleLoadService;
    private readonly InitialGameConfig initialGameConfig;

    [Inject]
    public BootstrapState(
      IGameStateMachine gameStateMachine,
      IStaticDataService staticDataService,
      IAssetBundleLoadService assetBundleLoadService,
      InitialGameConfig initialGameConfig
      )
    {
      this.gameStateMachine = gameStateMachine;
      this.staticDataService = staticDataService;
      this.initialGameConfig = initialGameConfig;
      this.assetBundleLoadService = assetBundleLoadService;
    }

    public void Enter()
    {
      staticDataService.LoadLevels();
      assetBundleLoadService.LoadAssetBundle(initialGameConfig.InitialBundleName);
      
      gameStateMachine.Enter<MainMenuState>();
    }

    public void Exit() { }
  }
}