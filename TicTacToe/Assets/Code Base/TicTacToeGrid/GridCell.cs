using Code_Base.Enums;

namespace Code_Base.TicTacToeGrid
{
  public class GridCell
  {
    public PlayerType OccupiedBy { get; private set; }

    private readonly Grid grid;

    public GridCell(Grid grid) => 
      this.grid = grid;

    public void OccupyBy(PlayerType playerType)
    {
      grid.NonOccupiedGridCells.Remove(this);
      OccupiedBy = playerType;
    }

    public void ClearGridCell()
    {
      grid.NonOccupiedGridCells.Add(this);
      OccupiedBy = PlayerType.NoPlayer;
    }
  }
}