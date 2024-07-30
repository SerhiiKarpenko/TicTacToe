using System.Collections;
using Code_Base.Infrastructure.CoroutineRunner;
using UnityEngine;

namespace Code_Base.UI.Curtain
{
  public class SceneLoadingCurtain : MonoBehaviour, ILoadingCurtain
  {
    [SerializeField] private CanvasGroup canvasGroup;
    private ICoroutineRunner coroutineRunner;

    private void Awake() => 
      DontDestroyOnLoad(this);

    public void Construct(ICoroutineRunner coroutineRunner) => 
      this.coroutineRunner = coroutineRunner;

    public void Show()
    {
      gameObject.SetActive(true);
      canvasGroup.alpha = 1f;
    }

    public void Hide() =>
      coroutineRunner.StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
      while (canvasGroup.alpha > 0)
      {
        canvasGroup.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      Destroy(gameObject);
    }
  }
}