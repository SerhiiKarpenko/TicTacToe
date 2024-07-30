using Code_Base.Player;

namespace Code_Base.UI.EndGameWindow
{
  public interface IEndGamePresenter : IPresenter
  {
    public void EnableWinObject(IPlayer winner);
    public void EnableDrawObject();

    public void Cleanup();
  }
}