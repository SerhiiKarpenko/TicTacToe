using UnityEngine;

namespace Code_Base.Infrastructure.Configs.SceneNames
{
  [CreateAssetMenu(fileName = "SceneNames", menuName = "Configs/Create Scene Names Config", order = 0)]
  public class SceneNames : ScriptableObject
  {
    public string MainGameSceneName;
    public string MainMenuSceneName;
  }
}