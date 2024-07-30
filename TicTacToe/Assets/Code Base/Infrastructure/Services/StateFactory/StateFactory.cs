using Zenject;

namespace Code_Base.Infrastructure.Services.StateFactory
{
  public class StateFactory : IStateFactory
  {
    private readonly IInstantiator instantiator;

    public StateFactory(IInstantiator instantiator) => 
      this.instantiator = instantiator;

    public TState CreateState<TState>() => 
      instantiator.Instantiate<TState>();
  }
}