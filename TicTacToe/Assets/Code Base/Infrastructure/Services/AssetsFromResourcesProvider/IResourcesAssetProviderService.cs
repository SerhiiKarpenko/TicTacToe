using Code_Base.Infrastructure.Services.ServiceInterfaces;
using UnityEngine;

namespace Code_Base.Infrastructure.Services.AssetsFromResourcesProvider
{
  public interface IResourcesAssetProviderService : IService
  {
    public T Load<T>(string path) where T : Object;
  }
}