using Code_Base.Infrastructure.Services.ServiceInterfaces;
using UnityEngine;

namespace Code_Base.Infrastructure.Services.TimeService
{
  public class TimeService : IService, ITimeService
  {
    public float DeltaTime 
      => Time.deltaTime;
  }
  
}