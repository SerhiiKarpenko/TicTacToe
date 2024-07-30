using System;
using System.IO;
using Code_Base.Utilities;
using UnityEngine;

namespace Code_Base.Infrastructure.Services.AssetBundleLoadService
{
  public class AssetBundleLoadService : IAssetBundleLoadService
  {
    public event Action OnNewAssetBundleLoaded = delegate { };
    public event Action OnAssetBundleUnloaded = delegate { };
    public AssetBundle CurrentAssetBundle { get; private set; }

    public void LoadAssetBundle(string assetBundleName)
    {
      if (TryingToLoadSameBundle(assetBundleName) || assetBundleName.IsNullOrEmpty() || !AssetBundleExist(assetBundleName))
      {
        return;
      }

      UnloadCurrentBundle();

      AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, assetBundleName));
      CurrentAssetBundle = assetBundle;
      
      OnNewAssetBundleLoaded.Invoke();
    }

    private void UnloadCurrentBundle()
    {
      if (CurrentAssetBundle != null)
      {
        CurrentAssetBundle.Unload(true);
        OnAssetBundleUnloaded.Invoke();
      }
    }

    private bool TryingToLoadSameBundle(string assetBundleName) => 
      CurrentAssetBundle != null && CurrentAssetBundle.name == assetBundleName;

    private bool AssetBundleExist(string assetBundleName) => 
      File.Exists(Path.Combine(Application.streamingAssetsPath, assetBundleName));
  }
  
  
}