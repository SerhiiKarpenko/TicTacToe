using Code_Base.Infrastructure.Services.TimerService;

namespace Code_Base.UI.Timer
{
  public interface IRoundTimerPresenter : IPresenter
  {
    public void SetupTimer(ITimer timer);
    public void Cleanup();
  }
}