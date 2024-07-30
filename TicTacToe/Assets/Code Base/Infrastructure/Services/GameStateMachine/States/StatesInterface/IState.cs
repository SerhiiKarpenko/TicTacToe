namespace Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface
{
  public interface IState : IExcitableState
  {
    public void Enter();
  }
  
  public interface IPayloadState<TPayload> : IExcitableState
  {
    public void Enter(TPayload payload);
  }

  public interface IExcitableState
  {
    public void Exit();
  }
}