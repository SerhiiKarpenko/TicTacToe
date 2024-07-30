using Code_Base.Infrastructure.Configs.AssetPaths;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using UnityEngine;
using Zenject;

namespace Code_Base.Infrastructure.Services.GridSignFactory
{
  public class GridSignFactory : IGridSignFactory
  {
    private readonly IAssetsFromBundleProviderService assetsFromBundleProvider;
    private readonly AssetBundleAssetsPaths assetBundleAssetsPaths;
    
    [Inject]
    public GridSignFactory(IAssetsFromBundleProviderService assetsFromBundleProvider, AssetBundleAssetsPaths assetBundleAssetsPaths)
    {
      this.assetBundleAssetsPaths = assetBundleAssetsPaths;
      this.assetsFromBundleProvider = assetsFromBundleProvider;
    }
    
    public Sprite CreateXSign()
    {
      Sprite xSignSprite = assetsFromBundleProvider.LoadAsset<Sprite>(assetBundleAssetsPaths.XSignPath);
      return xSignSprite;
    }

    public Sprite CreateOSign()
    {
      Sprite oSignPrefab = assetsFromBundleProvider.LoadAsset<Sprite>(assetBundleAssetsPaths.OSignPath);
      return oSignPrefab;
    }
  }
}