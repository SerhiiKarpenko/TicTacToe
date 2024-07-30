using System;
using System.Collections.Generic;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;

namespace Code_Base.Infrastructure.Services.GameStateMachine
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExcitableState> states = new();
    private IExcitableState activeState;
    
    public void Enter<TState>() where TState : class, IState
    {
      TState state = ChangeState<TState>();
      state.Enter();
    }
    
    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void RegisterState<TState>(TState state) where TState : class, IExcitableState => 
      states.Add(typeof(TState), state);
    
    private TState ChangeState<TState>() where TState : class, IExcitableState
    {
      activeState?.Exit();
      TState state = GetState<TState>();
      activeState = state;
      return state;
    }

    private TState GetState<TState>() where TState : class, IExcitableState => 
      states[typeof(TState)] as TState;
  }
}