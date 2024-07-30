using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine;
using Code_Base.Infrastructure.Services.GameStateMachine.States;
using Code_Base.Infrastructure.Services.HintService;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.Player;

namespace Code_Base.UI.MainHud
{
  public class MainHudPresenter : IMainHudPresenter
  {
    private readonly MainHudVisual mainHudVisual;
    private readonly IHintService hintService;
    private readonly IGameStateMachine gameStateMachine;
    private readonly ICommandService commandService;
    private readonly IPlayersOrderService playersOrderService;
    private readonly IGameSetupService gameSetupService;

    public MainHudPresenter(
      MainHudVisual mainHudVisual, 
      IHintService hintService,
      IGameStateMachine gameStateMachine,
      ICommandService commandService,
      IPlayersOrderService playersOrderService, 
      IGameSetupService gameSetupService
      )
    {
      this.mainHudVisual = mainHudVisual;
      this.hintService = hintService;
      this.gameStateMachine = gameStateMachine;
      this.commandService = commandService;
      this.playersOrderService = playersOrderService;
      this.gameSetupService = gameSetupService;
    }

    public void Initialize()
    {
      mainHudVisual.HintButton.onClick.AddListener(OnHintButtonClick);
      mainHudVisual.UndoButton.onClick.AddListener(OnUndoButtonClick);
      mainHudVisual.RestartButton.onClick.AddListener(OnRestartButtonClick);
      playersOrderService.OnCurrentPlayerChange += UpdateTurnText;
    }

    private void OnHintButtonClick() => 
      hintService.ShowHint();

    private void OnUndoButtonClick() => 
      commandService.UndoLastMove();

    private void OnRestartButtonClick()
    {
      gameSetupService.FirstPlayer.Deinitialize();
      gameSetupService.SecondPlayer.Deinitialize();
      
      gameStateMachine.Enter<GameLoopInitialState>();
    }

    private void UpdateTurnText(IPlayer currentPlayer)
    {
      mainHudVisual.PlayerTurnText.text = $"{currentPlayer.PlayerIdentifier().ToString()} {currentPlayer.SignType.ToString()} player turn";
    }
  }
}