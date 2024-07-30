using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Infrastructure.Services.GridFactory
{
  public interface IGridFactory : IService
  {
    public IGrid CreateGrid(int gridWidth, int gridHeight);
    public IGridPresenter CreateGridPresenter(IGrid grid, IGridVisual gridVisual);
    GridCellPresenter CreateGridCellPresenter(GridCell gridCell, GridCellVisual gridCellVisual);
  }
}