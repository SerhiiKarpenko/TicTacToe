using Code_Base.Infrastructure.Configs.ResourceAssetPaths;
using Code_Base.Infrastructure.CoroutineRunner;
using Code_Base.Infrastructure.Services.AssetsFromResourcesProvider;
using Code_Base.UI.Curtain;
using UnityEngine;
using Zenject;

namespace Code_Base.Infrastructure.Services.LoadingCurtainFactory
{
  public class LoadingCurtainFactory : ILoadingCurtainFactory
  {
    private readonly IResourcesAssetProviderService resourcesAssetProviderService;
    private readonly ICoroutineRunner coroutineRunner;
    private readonly ResourceAssetsPaths resourceAssetsPaths;

    [Inject]
    public LoadingCurtainFactory(
      ICoroutineRunner coroutineRunner,
      IResourcesAssetProviderService resourcesAssetProviderService,
      ResourceAssetsPaths resourceAssetsPaths
      )
    {
      this.coroutineRunner = coroutineRunner;
      this.resourcesAssetProviderService = resourcesAssetProviderService;
      this.resourceAssetsPaths = resourceAssetsPaths;
    }
    
    public ILoadingCurtain CreateSceneLoadingCurtain()
    {
      GameObject loadingCurtain = resourcesAssetProviderService.Load<GameObject>(resourceAssetsPaths.SceneLoadingCurtain);
      GameObject loadingCurtainClone = Object.Instantiate(loadingCurtain);
      
      SceneLoadingCurtain curtain = loadingCurtainClone.GetComponent<SceneLoadingCurtain>();
      curtain.Construct(coroutineRunner);
      
      return curtain;
    }
  }
}