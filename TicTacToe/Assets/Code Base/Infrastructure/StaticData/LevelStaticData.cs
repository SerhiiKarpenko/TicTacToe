using UnityEngine;

namespace Code_Base.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Static Data/Create Level Static Data")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    [Min(0)] public int GridWidht = 3;
    [Min(0)] public int GridHeight = 3;
  }
}