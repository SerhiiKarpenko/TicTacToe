using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.UI.Timer;

namespace Code_Base.Infrastructure.Services.TimerPresenterProvider
{
  public interface IRoundTimerPresenterProvider : IService
  {
    public IRoundTimerPresenter RoundTimerPresenter { get; }
    public void SetupTimerPresenter(IRoundTimerPresenter roundTimerPresenter);
  }
}