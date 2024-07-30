using System;
using Code_Base.Player;

namespace Code_Base.Infrastructure.Services.NextPlayerService
{
  public class PlayersOrderService : IPlayersOrderService
  {
    public event Action<IPlayer> OnCurrentPlayerChange;
    
    public IPlayer NextPlayer { get; private set; }
    public IPlayer CurrentPlayer { get; private set; }

    public void SetNextPlayer(IPlayer player) => 
      NextPlayer = player;

    public void SetCurrentPlayer(IPlayer player)
    {
      CurrentPlayer = player;
      OnCurrentPlayerChange?.Invoke(CurrentPlayer);
    }
  }
}