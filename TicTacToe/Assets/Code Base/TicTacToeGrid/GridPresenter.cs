using System;
using System.Collections.Generic;
using Code_Base.Infrastructure.Services.GridFactory;
using Code_Base.Utilities;

namespace Code_Base.TicTacToeGrid
{
  public class GridPresenter : IGridPresenter
  {
    public event Action OnGridCellOccupied = delegate { };
    public IGrid Grid { get; }

    private readonly IGridVisual gridVisual;
    private readonly GridCellPresenter[,] gridCellPresenters;
    private readonly List<GridCellPresenter> nonOccupiedCellsPresenters  = new();
    private readonly IGridFactory gridFactory;
    
    public GridPresenter(IGrid grid, IGridVisual gridVisual, IGridFactory gridFactory)
    {
      Grid = grid;
      this.gridVisual = gridVisual;
      this.gridFactory = gridFactory;
      gridCellPresenters = new GridCellPresenter[grid.GetGridSize().x, grid.GetGridSize().y];
    }

    public void InitializeGrid() => 
      Grid.InitializeGrid();

    public void InitializeGridVisual()
    {
      gridVisual.InitializeGridVisuals(Grid.GetGridSize().x, Grid.GetGridSize().y);
    }

    public void InitializeCellsPresenters()
    {
      for (int column = 0; column < Grid.GetGridSize().x; column++)
      {
        for (int row = 0; row < Grid.GetGridSize().y; row++)
        {
          GridCellPresenter gridCellPresenter = gridFactory
            .CreateGridCellPresenter(Grid.GridCells[column, row], gridVisual.GridCellVisuals[column, row]);

          gridCellPresenters[column, row] = gridCellPresenter;
          nonOccupiedCellsPresenters.Add(gridCellPresenter);
        }
      }
    }

    public void SubscribeOnCellsEvents()
    {
      foreach (GridCellPresenter cellPresenter in gridCellPresenters)
      {
        cellPresenter.OnGridCellOccupied += GridCellOccupied;
        cellPresenter.OnGridCellCleared += GridCellCleared;
      }
    }

    public GridCellPresenter GetRandomNonOccupiedGridCellPresenter() => 
      nonOccupiedCellsPresenters.PickRandom();

    public bool NoFreeCells() => 
      Grid.NonOccupiedGridCells.IsNullOrEmpty();

    public void ClearGrid()
    {
      nonOccupiedCellsPresenters.Clear();
      Grid.NonOccupiedGridCells.Clear();
      
      UnSubscribeOnCellsEvents();
      
      foreach (GridCellPresenter gridCellPresenter in gridCellPresenters)
      {
        nonOccupiedCellsPresenters.Add(gridCellPresenter);
        gridCellPresenter.ClearGridCell();
      }
    }

    public void SetGridEnabled(bool enabled)
    {
      foreach (GridCellPresenter gridCellPresenter in gridCellPresenters)
      {
        gridCellPresenter.SetGridCellEnabled(enabled);
      }
    }

    private void UnSubscribeOnCellsEvents()
    {
      foreach (GridCellPresenter cellPresenter in gridCellPresenters)
      {
        cellPresenter.OnGridCellOccupied -= GridCellOccupied;
        cellPresenter.OnGridCellCleared -= GridCellCleared;
      }
    }

    private void GridCellOccupied(GridCellPresenter gridCellPresenter)
    {
      nonOccupiedCellsPresenters.Remove(gridCellPresenter);
      OnGridCellOccupied?.Invoke();
    }

    private void GridCellCleared(GridCellPresenter gridCellPresenter) => 
      nonOccupiedCellsPresenters.Add(gridCellPresenter);
  }
}