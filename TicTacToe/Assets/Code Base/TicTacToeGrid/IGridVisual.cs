namespace Code_Base.TicTacToeGrid
{
  public interface IGridVisual
  {
    public GridCellVisual[,] GridCellVisuals { get; }
    public void InitializeGridVisuals(int gridWidth, int gridHeight);
  }
}