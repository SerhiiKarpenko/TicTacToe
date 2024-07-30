using System.Collections.Generic;
using UnityEngine;

namespace Code_Base.TicTacToeGrid
{
  public class Grid : IGrid
  {
    public GridCell[,] GridCells { get; }
    public List<GridCell> NonOccupiedGridCells { get; } = new();

    private readonly int gridWidth;
    private readonly int gridHeight;
    
    public Grid(int gridWidth, int gridHeight)
    {
      this.gridWidth = gridWidth;
      this.gridHeight = gridHeight;
      GridCells = new GridCell[gridWidth, gridHeight];
    }

    public void InitializeGrid()
    {
      for (int column = 0; column < gridWidth; column++)
      {
        for (int row = 0; row < gridHeight; row++)
        {
          GridCell gridCell = new GridCell(this);
          GridCells[column, row] = gridCell;
        }
      }
    }
    
    public Vector2Int GetGridSize() => 
      new(gridWidth, gridHeight);
  }
}
