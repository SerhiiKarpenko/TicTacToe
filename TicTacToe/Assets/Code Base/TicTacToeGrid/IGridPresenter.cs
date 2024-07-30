using System;

namespace Code_Base.TicTacToeGrid
{
  public interface IGridPresenter
  {
    public event Action OnGridCellOccupied; 
    
    IGrid Grid { get; }
    public void InitializeGrid();
    public void InitializeGridVisual();
    public void InitializeCellsPresenters();
    public void ClearGrid();
    public void SetGridEnabled(bool enabled);
    public void SubscribeOnCellsEvents();
    
    public GridCellPresenter GetRandomNonOccupiedGridCellPresenter();
    public bool NoFreeCells();
  }
}