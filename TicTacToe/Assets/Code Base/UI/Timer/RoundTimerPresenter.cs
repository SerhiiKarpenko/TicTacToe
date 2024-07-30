using System;
using Code_Base.Infrastructure.Services.TimerService;

namespace Code_Base.UI.Timer
{
  public class RoundTimerPresenter : IRoundTimerPresenter
  {
    private readonly RoundTimerVisual roundTimerVisual;
    private ITimer timer;

    public RoundTimerPresenter(RoundTimerVisual roundTimerVisual)
    {
      this.roundTimerVisual = roundTimerVisual;
    }

    public void Initialize()
    {
      if (timer == null)
      {
        throw new NullReferenceException("timer is null");
      }
      
      timer.OnTimerUpdate += UpdateTimerText;
      timer.OnTimerEnd += Cleanup;
    }
    
    public void SetupTimer(ITimer timer) => 
      this.timer = timer;

    public void Cleanup()
    {
      timer.OnTimerUpdate -= UpdateTimerText;
      timer.OnTimerEnd -= Cleanup;
      UpdateTimerText(0.0f);
    }

    private void UpdateTimerText(float currentTimerTime)
    {
      if (currentTimerTime <= 0)
      {
        roundTimerVisual.TimerText.text = "0.00";
      }
      
      roundTimerVisual.TimerText.text = currentTimerTime.ToString("F");
    }
  }
}
