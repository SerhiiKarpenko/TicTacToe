using System;
using System.Collections.Generic;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Object = UnityEngine.Object;

namespace Code_Base.Infrastructure.Services.AssetsFromBundleProvider
{
  public class AssetsFromBundleProviderService : IAssetsFromBundleProviderService
  {
    private readonly Dictionary<string, Object> loadedAssets = new();
    private readonly IAssetBundleLoadService assetBundleLoadService;

    public AssetsFromBundleProviderService(IAssetBundleLoadService assetBundleLoadService)
    {
      this.assetBundleLoadService = assetBundleLoadService;
      this.assetBundleLoadService.OnAssetBundleUnloaded += Cleanup;
    }

    public T LoadAsset<T>(string assetName) where T : Object
    {
      if (loadedAssets.TryGetValue(assetName, out Object loadedAsset))
      {
        return loadedAsset as T;
      }
      
      T asset = assetBundleLoadService.CurrentAssetBundle.LoadAsset<T>(assetName);

      if (asset == null)
      {
        throw new ArgumentException($"Load from asset bundle failed. Asset name: {assetName}");
      }
      
      loadedAssets.Add(assetName, asset);
      
      return asset;
    }

    public void Cleanup() => 
      loadedAssets.Clear();
  }
}