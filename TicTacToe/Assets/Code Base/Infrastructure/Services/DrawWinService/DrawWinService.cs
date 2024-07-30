using Code_Base.Enums;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Infrastructure.Services.DrawWinService
{
  public class DrawWinService : IDrawWinService
  {
    private IGrid grid;
    
    public bool CheckWin() =>
      CheckLeftDiagonal()|| CheckRightDiagonal() || CheckRowsForWin() || CheckColumnsForWin();

    public bool CheckDraw() => 
      NoCellsLeft();

    public void SetupGrid(IGrid grid) => 
      this.grid = grid;

    private bool CheckLeftDiagonal()
    {
      if (CellEmpty(grid.GridCells[0, 0]))
      {
        return false;
      }

      PlayerType occupiedBy = grid.GridCells[0, 0].OccupiedBy;

      for (int i = 1; i < grid.GetGridSize().x; i++)
      {
        if (grid.GridCells[i, i].OccupiedBy != occupiedBy)
        {
          return false;
        }
      }
      
      return true;
    }

    private bool CheckRightDiagonal()
    {
      if (CellEmpty(grid.GridCells[0, grid.GetGridSize().x - 1]))
      {
        return false;
      }

      PlayerType occupiedBy = grid.GridCells[0, grid.GetGridSize().x - 1].OccupiedBy;

      for (int i = 1; i < grid.GetGridSize().x; i++)
      {
        if (grid.GridCells[i, grid.GetGridSize().x - i - 1].OccupiedBy != occupiedBy)
        {
          return false;
        }
      }
      
      return true;
    }

    private bool CheckRowsForWin()
    {
      for (int row = 0; row < grid.GetGridSize().x; row++)
      {
        GridCell rowCell = grid.GridCells[row, 0];
        
        if (CellEmpty(rowCell))
        {
          continue;
        }
        
        PlayerType playerType = rowCell.OccupiedBy;
        bool win = true;
        
        for (int column = 1; column < grid.GetGridSize().x; column++)
        {
          if (grid.GridCells[row, column].OccupiedBy != playerType)
          {
            win = false;
            break;
          }
        }
        
        if (win)
        {
          return true;
        }
      }
      return false;
    }

    private bool CheckColumnsForWin()
    {
      for (int column = 0; column < grid.GetGridSize().x; column++)
      {
        GridCell columnCell = grid.GridCells[0, column];
        
        if (CellEmpty(columnCell))
        {
          continue;
        }
        
        PlayerType playerType = columnCell.OccupiedBy;
        bool win = true;

        for (int row = 1; row < grid.GetGridSize().y; row++)
        {
          if (grid.GridCells[row, column].OccupiedBy != playerType)
          {
            win = false;
            break;
          }
        }

        if (win)
        {
          return true;
        }
      }

      return false;
    }
    

    private bool NoCellsLeft() => 
      grid.NonOccupiedGridCells.Count <= 0;

    private bool CellEmpty(GridCell gridCell) => 
      gridCell.OccupiedBy == PlayerType.NoPlayer;
  }
}