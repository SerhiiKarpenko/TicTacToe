using Code_Base.Infrastructure.Services.GameStateMachine;
using Code_Base.Infrastructure.Services.GameStateMachine.States;
using Code_Base.Player;
using Object = UnityEngine.Object;

namespace Code_Base.UI.EndGameWindow
{
  public class EndGamePresenter : IEndGamePresenter
  {
    private readonly EndGameVisual endGameVisual;
    private readonly IGameStateMachine gameStateMachine;

    public EndGamePresenter(EndGameVisual endGameVisual, IGameStateMachine gameStateMachine)
    {
      this.endGameVisual = endGameVisual;
      this.gameStateMachine = gameStateMachine;
    }

    public void Initialize()
    {
      endGameVisual.MainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
      endGameVisual.RestartButton.onClick.AddListener(OnRestartButtonClick);
    }

    public void EnableWinObject(IPlayer winner)
    {
      endGameVisual.SetWinnerText($"{winner.SignType.ToString()} WINS!" );
      endGameVisual.WinGameObject.SetActive(true);
    }
    
    public void EnableDrawObject() => 
      endGameVisual.DrawGameObject.SetActive(true);

    public void Cleanup()
    {
      endGameVisual.MainMenuButton.onClick.RemoveAllListeners();
      endGameVisual.RestartButton.onClick.RemoveAllListeners();
      
      Object.Destroy(endGameVisual.gameObject);
    }

    private void OnMainMenuButtonClick() => 
      gameStateMachine.Enter<MainMenuState>();

    private void OnRestartButtonClick() => 
      gameStateMachine.Enter<GameLoopInitialState>();
  }
}