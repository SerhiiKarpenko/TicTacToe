using System.Collections;
using UnityEngine;

namespace Code_Base.Infrastructure.CoroutineRunner
{
  public interface ICoroutineRunner
  {
    public Coroutine StartCoroutine(IEnumerator coroutine);
  }
}