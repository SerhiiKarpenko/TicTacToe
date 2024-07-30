using System;
using Code_Base.Infrastructure.Services.ServiceInterfaces;

namespace Code_Base.Infrastructure.Services.SceneLoader
{
  public interface ISceneLoader : IService
  {
    public void Load(string name, Action onLoaded);
  }
}