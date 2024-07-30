using System;
using Code_Base.Infrastructure.Services.TimeService;

namespace Code_Base.Infrastructure.Services.TimerService
{
  public class Timer : ITimer
  {
    public event Action OnTimerEnd = delegate { };
    public event Action<float> OnTimerUpdate;

    private bool timerStarted;
    private float currentTime;
    
    private readonly float timerEndTime;
    
    private readonly ITimerService timerService;
    private readonly ITimeService timeService;
    
    public Timer(ITimerService timerService, ITimeService timeService, float timerEndTime)
    {
      this.timerService = timerService;
      this.timeService = timeService;
      this.timerEndTime = timerEndTime;
    }

    public void StartTimer(float startTime)
    {
      timerStarted = true;
      currentTime = startTime;
      timerService.OnTick += UpdateTimer;
    }

    private void UpdateTimer()
    {
      if (!timerStarted)
      {
        return;
      }
      
      if (currentTime <= timerEndTime)
      {
        OnTimerEnd?.Invoke();
        timerStarted = false;
        timerService.OnTick -= UpdateTimer;
        return;
      }

      currentTime -= timeService.DeltaTime;
      OnTimerUpdate?.Invoke(currentTime);
    }
  }
}