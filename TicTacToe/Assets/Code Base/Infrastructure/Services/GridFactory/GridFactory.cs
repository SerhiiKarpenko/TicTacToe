using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.GridSignFactory;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.TicTacToeGrid;
using Zenject;

namespace Code_Base.Infrastructure.Services.GridFactory
{
  public class GridFactory : IGridFactory
  {
    private readonly IGridSignFactory gridSignFactory;
    private readonly IPlayersOrderService playersOrderService;
    private readonly ICommandService commandService;
    
    [Inject]
    public GridFactory(IPlayersOrderService playersOrderService, IGridSignFactory gridSignFactory, ICommandService commandService)
    {
      this.playersOrderService = playersOrderService;
      this.gridSignFactory = gridSignFactory;
      this.commandService = commandService;
    }
    
    public IGrid CreateGrid(int gridWidth, int gridHeight) => 
      new Grid(gridWidth, gridHeight);

    public IGridPresenter CreateGridPresenter(IGrid grid, IGridVisual gridVisual) => 
      new GridPresenter(grid, gridVisual, this);

    public GridCellPresenter CreateGridCellPresenter(GridCell gridCell, GridCellVisual gridCellVisual)
    {
      GridCellPresenter gridCellPresenter = new GridCellPresenter(gridCell, gridCellVisual, playersOrderService, gridSignFactory, commandService);
      gridCellPresenter.Initialize();
      
      return gridCellPresenter;
    }
  }
}