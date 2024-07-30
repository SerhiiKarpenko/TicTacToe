using Code_Base.Infrastructure.Configs.InfrastructureResourcesPaths;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameBootstrapperFactory
{
  public class GameBootstrapperFactory : IGameBootstrapperFactory
  {
    private readonly DiContainer diContainer;
    private readonly InfrastructureResourcesPaths infrastructureResourcesPaths;

    [Inject]
    public GameBootstrapperFactory(DiContainer diContainer, InfrastructureResourcesPaths infrastructureResourcesPaths)
    {
      this.diContainer = diContainer;
      this.infrastructureResourcesPaths = infrastructureResourcesPaths;
    }

    public void CreateGameBootstrapper() => 
      diContainer.InstantiatePrefabResource(infrastructureResourcesPaths.BootstrapperPath);
  }
}