using Code_Base.Infrastructure.Services.DrawWinService;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Zenject;

namespace Code_Base.Tests
{
  public class WinDrawLoseTestInstaller : Installer<WinDrawLoseTestInstaller>
  {
    public override void InstallBindings()
    {
      Container.Bind<IDrawWinService>().To<DrawWinService>().AsSingle();
    }
  }
}