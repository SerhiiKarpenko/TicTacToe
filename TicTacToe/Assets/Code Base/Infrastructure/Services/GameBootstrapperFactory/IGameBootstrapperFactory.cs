using Code_Base.Infrastructure.Services.ServiceInterfaces;

namespace Code_Base.Infrastructure.Services.GameBootstrapperFactory
{
  public interface IGameBootstrapperFactory : IService
  {
    public void CreateGameBootstrapper();
  }
}