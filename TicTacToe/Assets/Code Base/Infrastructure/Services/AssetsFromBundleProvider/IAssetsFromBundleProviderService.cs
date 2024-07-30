using Code_Base.Infrastructure.Services.ServiceInterfaces;
using UnityEngine;

namespace Code_Base.Infrastructure.Services.AssetsFromBundleProvider
{
  public interface IAssetsFromBundleProviderService : IService
  {
    public T LoadAsset<T>(string assetName) where T : Object;
    public void Cleanup();
  }
}