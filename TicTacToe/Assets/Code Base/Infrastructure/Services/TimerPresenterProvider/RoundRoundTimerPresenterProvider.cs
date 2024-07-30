using Code_Base.UI.Timer;

namespace Code_Base.Infrastructure.Services.TimerPresenterProvider
{
  public class RoundRoundTimerPresenterProvider : IRoundTimerPresenterProvider
  {
    public IRoundTimerPresenter RoundTimerPresenter { get; private set; }

    public void SetupTimerPresenter(IRoundTimerPresenter roundTimerPresenter) => 
      RoundTimerPresenter = roundTimerPresenter;
  }
}