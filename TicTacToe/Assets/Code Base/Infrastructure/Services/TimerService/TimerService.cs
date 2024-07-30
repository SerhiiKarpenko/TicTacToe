using System;
using System.Collections.Generic;
using Code_Base.Infrastructure.Services.TimeService;
using UnityEngine;
using Zenject;

namespace Code_Base.Infrastructure.Services.TimerService
{
  public class TimerService : ITimerService, ITickable
  {
    private const float END_TIMER_TIME = 0.0f;
    
    public event Action OnTick = delegate { };
    
    private bool timerStarted;
    private float currentTime;

    private readonly ITimeService timeService;
    
    public TimerService(ITimeService timeService)
    {
      this.timeService = timeService;
    }

    public void Tick()
    {
      OnTick.Invoke();
    }

    public ITimer CreateTimer()
    {
      ITimer timer = new Timer(this, timeService, END_TIMER_TIME);
      return timer;
    }
  }
}