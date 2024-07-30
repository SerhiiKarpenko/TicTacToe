using Code_Base.TicTacToeGrid;

namespace Code_Base.Infrastructure.Services.GridPresenterProvider
{
  public interface IGridPresenterProvider
  {
    public IGridPresenter GridPresenter { get; }
    public void SetupGridPresenter(IGridPresenter gridPresenter);
  }
}