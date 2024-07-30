using Code_Base.TicTacToeGrid;

namespace Code_Base.Infrastructure.Services.GridPresenterProvider
{
  public class GridPresenterProvider : IGridPresenterProvider
  {
    public IGridPresenter GridPresenter { get; private set; }

    public void SetupGridPresenter(IGridPresenter gridPresenter) => 
      GridPresenter = gridPresenter;
  }
}