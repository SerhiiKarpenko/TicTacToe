using UnityEngine;

namespace Code_Base.Infrastructure.CoroutineRunner
{
  public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
  {
    private void Awake() => 
      DontDestroyOnLoad(this);
  }
}