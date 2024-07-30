using Code_Base.Infrastructure.Configs.AssetPaths;
using Code_Base.Infrastructure.Services.AssetBundleLoadService;
using Code_Base.Infrastructure.Services.AssetsFromBundleProvider;
using UnityEngine;

namespace Code_Base.UI.Decorataion
{
  public class DecorationBackgroundPresenter : IDecorationBackgroundPresenter
  {
    private readonly DecorationBackgroundVisual decorationBackgroundVisual;
    private readonly IAssetBundleLoadService assetBundleLoadService;
    private readonly IAssetsFromBundleProviderService assetsFromBundleProviderService;
    private readonly AssetBundleAssetsPaths assetBundleAssetsPaths;

    public DecorationBackgroundPresenter(
      DecorationBackgroundVisual decorationBackgroundVisual,
      IAssetBundleLoadService assetBundleLoadService,
      AssetBundleAssetsPaths assetBundleAssetsPaths, IAssetsFromBundleProviderService assetsFromBundleProviderService)
    {
      this.decorationBackgroundVisual = decorationBackgroundVisual;
      this.assetBundleLoadService = assetBundleLoadService;
      this.assetBundleAssetsPaths = assetBundleAssetsPaths;
      this.assetsFromBundleProviderService = assetsFromBundleProviderService;
    }

    public void Initialize()
    {
      SubscribeOnNewBundleLoaded();
      UpdateBackgroundImage();
    }

    public void Cleanup() => 
      assetBundleLoadService.OnNewAssetBundleLoaded -= UpdateBackgroundImage;

    private void SubscribeOnNewBundleLoaded() => 
      assetBundleLoadService.OnNewAssetBundleLoaded += UpdateBackgroundImage;

    private void UpdateBackgroundImage()
    {
      decorationBackgroundVisual.DecortaionImage.sprite = 
        assetsFromBundleProviderService.LoadAsset<Sprite>(assetBundleAssetsPaths.BackgroundPath);
    }
  }
}