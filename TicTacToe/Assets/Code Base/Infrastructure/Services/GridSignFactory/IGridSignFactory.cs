using Code_Base.Infrastructure.Services.ServiceInterfaces;
using UnityEngine;

namespace Code_Base.Infrastructure.Services.GridSignFactory
{
  public interface IGridSignFactory : IService
  {
    public Sprite CreateXSign();
    public Sprite CreateOSign();
  }
}