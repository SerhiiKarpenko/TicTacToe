using UnityEngine;

namespace Code_Base.Infrastructure.Configs.InfrastructureResourcesPaths
{
  [CreateAssetMenu(fileName = "InfrastructureAssetPaths", menuName = "Configs/Create Infrastructure Asset Paths")]
  public class InfrastructureResourcesPaths : ScriptableObject
  {
    public string BootstrapperPath = "";
    public string CoroutineRunnerPath = "";
  }
}