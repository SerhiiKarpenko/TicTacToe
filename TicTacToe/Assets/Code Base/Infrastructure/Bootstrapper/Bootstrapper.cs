using Code_Base.Infrastructure.Services.GameStateMachine;
using Code_Base.Infrastructure.Services.GameStateMachine.States;
using Code_Base.Infrastructure.Services.StateFactory;
using UnityEngine;
using Zenject;

namespace Code_Base.Infrastructure.Bootstrapper
{
  public class Bootstrapper : MonoBehaviour
  {
    private IStateFactory stateFactory;
    private IGameStateMachine gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, IStateFactory stateFactory)
    {
      this.gameStateMachine = gameStateMachine;
      this.stateFactory = stateFactory;
    }

    private void Start()
    {
      gameStateMachine.RegisterState(stateFactory.CreateState<BootstrapState>());
      gameStateMachine.RegisterState(stateFactory.CreateState<MainMenuState>());
      gameStateMachine.RegisterState(stateFactory.CreateState<LoadMainGameSceneState>());
      gameStateMachine.RegisterState(stateFactory.CreateState<GameLoopInitialState>());
      gameStateMachine.RegisterState(stateFactory.CreateState<PlayerTurnState>());
      gameStateMachine.RegisterState(stateFactory.CreateState<WinState>());
      gameStateMachine.RegisterState(stateFactory.CreateState<DrawState>());
      
      gameStateMachine.Enter<BootstrapState>();
      
      DontDestroyOnLoad(gameObject);
    }
  }
}