using System;
using Code_Base.Infrastructure.Services.ServiceInterfaces;

namespace Code_Base.Infrastructure.Services.TimerService
{
  public interface ITimerService : IService
  {
    public event Action OnTick;
    public ITimer CreateTimer();
  }
}