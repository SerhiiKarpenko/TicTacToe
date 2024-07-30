using System;
using Code_Base.Enums;

namespace Code_Base.Player
{
  public interface IPlayer
  {
    public event Action OnPlayerMadeMove;
    public SignType SignType { get; }
    public PlayerType PlayerType { get; }
    
    
    public PlayerIdentifier PlayerIdentifier();
    public void StartMove();
    public void Deinitialize();

  }
}