using System;

namespace Code_Base.Infrastructure.Services.TimerService
{
  public interface ITimer
  {
    public event Action OnTimerEnd;
    public event Action<float> OnTimerUpdate;
    
    public void StartTimer(float startTime);
  }
}