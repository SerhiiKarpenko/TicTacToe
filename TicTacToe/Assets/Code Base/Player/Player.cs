using System;
using Code_Base.Enums;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Player
{
  public abstract class Player : IPlayer
  {
    public event Action OnPlayerMadeMove = delegate { };
    public SignType SignType { get; }
    public PlayerType PlayerType { get; }


    protected IGridPresenter gridPresenter;

    protected Player(SignType signType, PlayerType playerType, IGridPresenter gridPresenter)
    {
      this.SignType = signType;
      PlayerType = playerType;
      this.gridPresenter = gridPresenter;
    }

    public abstract void StartMove(); 
    public abstract void Deinitialize();
    public abstract PlayerIdentifier PlayerIdentifier();


    protected virtual void EndMove()
    {
      OnPlayerMadeMove.Invoke();
    }
  }
}