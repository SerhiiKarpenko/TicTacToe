using Code_Base.Infrastructure.Services.ServiceInterfaces;

namespace Code_Base.Infrastructure.Services.HintService
{
  public interface IHintService : IService
  {
    public void ShowHint();
  }
}