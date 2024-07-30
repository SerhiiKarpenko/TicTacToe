using System;
using Code_Base.Infrastructure.Services.ServiceInterfaces;
using UnityEngine;

namespace Code_Base.Infrastructure.Services.AssetBundleLoadService
{
  public interface IAssetBundleLoadService : IService
  {
    public event Action OnNewAssetBundleLoaded;
    public event Action OnAssetBundleUnloaded;
    
    public AssetBundle CurrentAssetBundle { get; }
    public void LoadAssetBundle(string assetBundleName);
  }
}