using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.UI.Curtain;

namespace Code_Base.Infrastructure.Services.LoadingCurtainFactory
{
  public interface ILoadingCurtainFactory : IService
  {
    public ILoadingCurtain CreateSceneLoadingCurtain();
  }
}