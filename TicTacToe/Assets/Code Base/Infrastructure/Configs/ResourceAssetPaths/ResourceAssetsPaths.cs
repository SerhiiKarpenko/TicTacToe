using UnityEngine;

namespace Code_Base.Infrastructure.Configs.ResourceAssetPaths
{
  [CreateAssetMenu(fileName = "ResourceAssetsPaths", menuName = "Configs/Create Resource Assets Paths", order = 0)]
  public class ResourceAssetsPaths : ScriptableObject
  {
    public string SceneLoadingCurtain;
    public string UIRootPath;
    public string GridPanelPath;
    public string GridCellVisualPath;
    public string MainMenuVisualPath;
    public string EndGameVisualPath;
    public string MainHudVisualPath;
    public string DecorationVisualPath;
    public string TimerVisualPath;
  }
}