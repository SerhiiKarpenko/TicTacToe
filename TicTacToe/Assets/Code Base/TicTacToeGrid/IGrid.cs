using System.Collections.Generic;
using UnityEngine;

namespace Code_Base.TicTacToeGrid
{
  public interface IGrid
  {
    public GridCell[,] GridCells { get; } 
    public List<GridCell> NonOccupiedGridCells { get; }
    public void InitializeGrid();
    public Vector2Int GetGridSize();

  }
}