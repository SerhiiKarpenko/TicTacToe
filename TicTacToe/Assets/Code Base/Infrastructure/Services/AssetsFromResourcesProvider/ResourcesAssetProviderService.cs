using UnityEngine;

namespace Code_Base.Infrastructure.Services.AssetsFromResourcesProvider
{
  public class ResourcesAssetProviderService : IResourcesAssetProviderService
  {
    public T Load<T>(string path) where T : Object => 
      Resources.Load<T>(path);
  }
}
