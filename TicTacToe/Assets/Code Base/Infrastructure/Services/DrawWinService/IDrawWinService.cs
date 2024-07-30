using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Infrastructure.Services.DrawWinService
{
  public interface IDrawWinService : IService
  {
    bool CheckWin();
    bool CheckDraw();

    public void SetupGrid(IGrid grid);
  }
}