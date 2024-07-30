using Code_Base.TicTacToeGrid;

namespace Code_Base.Command
{
  public class MoveCommand : IMoveCommand
  {
    private readonly GridCellPresenter gridCellPresenter;

    public MoveCommand(GridCellPresenter gridCellPresenter)
    {
      this.gridCellPresenter = gridCellPresenter;
    }

    public void Execute() => 
        gridCellPresenter.OccupyGridCell();

    public void Undo() => 
      gridCellPresenter.ClearGridCell();
  }
}