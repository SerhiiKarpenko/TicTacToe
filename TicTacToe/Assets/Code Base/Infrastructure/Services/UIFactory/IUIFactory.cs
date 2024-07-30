using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.TicTacToeGrid;
using Code_Base.UI.Decorataion;
using Code_Base.UI.EndGameWindow;
using Code_Base.UI.MainHud;
using Code_Base.UI.MainMenu;
using Code_Base.UI.Timer;

namespace Code_Base.Infrastructure.Services.UIFactory
{
  public interface IUIFactory : IService
  {
    public void CreateUIRoot();
    public GridVisual CreateGridPanel();
    public GridCellVisual CreateGirdCellVisual();
    public MainMenuVisual CreateMainMenu();
    public EndGameVisual CreateEndGameMenu();
    public MainHudVisual CreateMainHud();
    public DecorationBackgroundVisual CreateDecorationBackground();
    public RoundTimerVisual CreateTimerVisual();

  }
}