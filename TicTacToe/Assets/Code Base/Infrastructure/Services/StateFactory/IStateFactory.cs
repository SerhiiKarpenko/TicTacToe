namespace Code_Base.Infrastructure.Services.StateFactory
{
  public interface IStateFactory
  {
    public TState CreateState<TState>();
  }
}