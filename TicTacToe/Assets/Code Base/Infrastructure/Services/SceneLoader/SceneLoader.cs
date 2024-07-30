using System;
using System.Collections;
using Code_Base.Infrastructure.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code_Base.Infrastructure.Services.SceneLoader
{
  public class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner coroutineRunner;
    

    [Inject]
    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      this.coroutineRunner = coroutineRunner;
    }

    public void Load(string name, Action onLoaded)
    {
      coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    }

    private IEnumerator LoadScene(string name, Action onLoaded)
    {
      if (SceneManager.GetActiveScene().name.Equals(name))
      {
        onLoaded?.Invoke();
        yield break;
      }

      AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(name);
      while (!loadSceneAsync.isDone)
      {
        yield return null;
      }
      
      onLoaded?.Invoke();
    }
  }
}