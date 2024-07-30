using Code_Base.Enums;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Player
{
  public class HumanPlayer : Player
  {
    public HumanPlayer(SignType signType, PlayerType playerType, IGridPresenter gridPresenter) : base(signType, playerType, gridPresenter) { }

    public override PlayerIdentifier PlayerIdentifier() => 
      Enums.PlayerIdentifier.Human;

    public override void StartMove() => 
      gridPresenter.OnGridCellOccupied += EndMove;

    public override void Deinitialize() => 
      gridPresenter.OnGridCellOccupied -= EndMove;

    protected override void EndMove()
    {
      Deinitialize();
      base.EndMove();
    }
  }
}