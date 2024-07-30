using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Infrastructure.Services.CommandService
{
  public interface ICommandService : IDisposableService
  {
    public void MakeMoveCommand(GridCellPresenter gridCellPresenter);
    public void UndoLastMove();
  }
}