using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.ServiceInterfaces;

namespace Code_Base.Infrastructure.Services.GameStateMachine
{
  public interface IGameStateMachine : IService
  {
    public void Enter<TState>() where TState : class, IState;
    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>;
    public void RegisterState<TState>(TState state) where TState : class, IExcitableState;
  }
}