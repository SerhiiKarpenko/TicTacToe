using System.IO;
using Code_Base.Infrastructure.Configs.AssetPaths;
using UnityEditor;
using UnityEngine;

namespace Editor
{
  #if UNITY_EDITOR
  public class AssetBundleBuilder : EditorWindow
  {
    private AssetBundleAssetsPaths assetBundleAssetsPaths;
    private Sprite xSymbol;
    private Sprite oSymbol;
    private Sprite background;
    private string assetBundleName;

    [MenuItem("Window/Custom/Asset Bundle Builder")]
    public static void ShowWindow()
    {
        GetWindow<AssetBundleBuilder>("Asset Bundle Builder");
    }

    private void OnGUI()
    {
        GUILayout.Label("Asset Bundle Settings", EditorStyles.boldLabel);

        assetBundleAssetsPaths = (AssetBundleAssetsPaths)EditorGUILayout.ObjectField("Asset names to set", assetBundleAssetsPaths, typeof(AssetBundleAssetsPaths), false);
        xSymbol = (Sprite)EditorGUILayout.ObjectField("X Symbol", xSymbol, typeof(Sprite), false);
        oSymbol = (Sprite)EditorGUILayout.ObjectField("O Symbol", oSymbol, typeof(Sprite), false);
        background = (Sprite)EditorGUILayout.ObjectField("Background", background, typeof(Sprite), false);

        assetBundleName = EditorGUILayout.TextField("Asset Bundle Name", assetBundleName);

        if (GUILayout.Button("Build Asset Bundle"))
        {
            BuildAssetBundle();
        }
    }

    private void BuildAssetBundle()
    {
      string assetBundleDirectory = Application.streamingAssetsPath;
      
      if (string.IsNullOrEmpty(assetBundleName))
      {
        Debug.LogError("Asset bundle name is empty!");
        return;
      }

      if (assetBundleAssetsPaths == null)
      {
        Debug.LogError("Asset names to set is null. Set or create ASSET BUNDLE ASSET PATHS");
        return;
      }

      if (IsTestsRelated(assetBundleAssetsPaths))
      {
        Debug.LogError($"Asset names is related to tests cant build bundles with: {assetBundleAssetsPaths.name}");
        return;
      }
      
      if (xSymbol == null || oSymbol == null || background == null)
      {
        Debug.LogError($"Cant build bundle, one of the assets is null | " +
                       $"xSymbol is null = {xSymbol == null} | ySymbol is null = {oSymbol == null} | background is null = {background == null}");
        return;
      }

      if (IsBuiltInAsset(xSymbol) || IsBuiltInAsset(oSymbol) || IsBuiltInAsset(background))
      {
        Debug.LogError("Cant build bundle with built in assets");
        return;
      }
      
      
      if (File.Exists(Path.Combine(assetBundleDirectory, assetBundleName)))
      {
        Debug.LogError("Bundle with such name exists");
        return;
      }

      if (!Directory.Exists(assetBundleDirectory))
      {
        Directory.CreateDirectory(assetBundleDirectory);
      }
      
      string[] bundleAssetNames = {
        assetBundleAssetsPaths.XSignPath,
        assetBundleAssetsPaths.OSignPath,
        assetBundleAssetsPaths.BackgroundPath
      };

      AssetBundleBuild build = new AssetBundleBuild
      {
        assetBundleName = assetBundleName,
        assetNames = new []
        {
          AssetDatabase.GetAssetPath(xSymbol),
          AssetDatabase.GetAssetPath(oSymbol),
          AssetDatabase.GetAssetPath(background)
        },
        addressableNames = bundleAssetNames
      };

      BuildPipeline.BuildAssetBundles(assetBundleDirectory, new[] { build }, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);


      AssetDatabase.Refresh();
    }
    
    private bool IsBuiltInAsset(Object asset)
    {
      string assetPath = AssetDatabase.GetAssetPath(asset);
      return assetPath.StartsWith("Resources/unity_builtin_extra") || assetPath.StartsWith("Library/unity default resources");
    }

    private bool IsTestsRelated(Object asset)
    {
      string assetPath = AssetDatabase.GetAssetPath(asset);
      return assetPath.Contains("UnitTests");
    }
  }
  #endif
}

