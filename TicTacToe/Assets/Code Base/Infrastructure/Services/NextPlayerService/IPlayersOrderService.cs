using System;
using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.Player;

namespace Code_Base.Infrastructure.Services.NextPlayerService
{
  public interface IPlayersOrderService : IService
  {
    public event Action<IPlayer> OnCurrentPlayerChange; 
    
    public IPlayer NextPlayer { get; }
    public IPlayer CurrentPlayer { get; }
    
    public void SetNextPlayer(IPlayer player);
    void SetCurrentPlayer(IPlayer player);
  }
}