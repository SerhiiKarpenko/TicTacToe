using UnityEngine;

namespace Code_Base.Infrastructure.Configs.AssetPaths
{
  [CreateAssetMenu(fileName = "AssetBundleAssetsNames", menuName = "Configs/Create Asset Bundle Assets Paths")]
  public class AssetBundleAssetsPaths : ScriptableObject
  {
    public string XSignPath;
    public string OSignPath;
    public string BackgroundPath;
  }
}