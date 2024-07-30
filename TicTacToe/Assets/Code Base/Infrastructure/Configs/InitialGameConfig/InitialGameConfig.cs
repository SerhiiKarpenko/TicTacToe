using Code_Base.Enums;
using UnityEngine;

namespace Code_Base.Infrastructure.Configs.InitialGameConfig
{
  [CreateAssetMenu(fileName = "InitialGameConfig", menuName = "Configs/Create Initial Game Config")]
  public class InitialGameConfig : ScriptableObject
  {
    public GameMode GameMode;
    [Min(1)] public int OneRoundTime;
    [Min(0)] public float ComputerThinkTime;
    public string InitialBundleName;
  }
}