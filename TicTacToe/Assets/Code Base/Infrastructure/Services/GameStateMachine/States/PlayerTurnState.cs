using Code_Base.Enums;
using Code_Base.Infrastructure.Configs.InitialGameConfig;
using Code_Base.Infrastructure.Services.DrawWinService;
using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.Infrastructure.Services.TimerPresenterProvider;
using Code_Base.Infrastructure.Services.TimerService;
using Code_Base.Infrastructure.Services.UIFactory;
using Code_Base.Player;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameStateMachine.States
{
  public class PlayerTurnState : IPayloadState<IPlayer>
  {
    private IPlayer player;
    private ITimer timer;
    
    private readonly IGameStateMachine gameStateMachine;
    private readonly ITimerService timerService;
    private readonly IPlayersOrderService playersOrderService;
    private readonly IGridPresenterProvider gridPresenterProvider;
    private readonly IDrawWinService drawWinService;
    private readonly IGameSetupService gameSetupService;
    private readonly IRoundTimerPresenterProvider roundTimerPresenterProvider;
    private readonly InitialGameConfig initialGameConfig;

    [Inject]
    public PlayerTurnState(
      IGameStateMachine gameStateMachine,
      ITimerService timerService,
      IPlayersOrderService playersOrderService,
      IGridPresenterProvider gridPresenterProvider,
      IDrawWinService drawWinService,
      IGameSetupService gameSetupService, 
      IRoundTimerPresenterProvider roundTimerPresenterProvider,
      InitialGameConfig initialGameConfig)
    {
      this.gameStateMachine = gameStateMachine;
      this.timerService = timerService;
      this.playersOrderService = playersOrderService;
      this.gridPresenterProvider = gridPresenterProvider;
      this.drawWinService = drawWinService;
      this.gameSetupService = gameSetupService;
      this.roundTimerPresenterProvider = roundTimerPresenterProvider;
      this.initialGameConfig = initialGameConfig;
    }

    public void Enter(IPlayer player)
    {
      this.player = player;
      this.player.OnPlayerMadeMove += EndPlayerTurn;
      this.player.StartMove();
      
      if (IsHumanTurn())
      {
        gridPresenterProvider.GridPresenter.SetGridEnabled(true);
      }
      
      if (gameSetupService.GameMode == GameMode.PlayerVsComputer)
      {
        InitializeTimer();
        InitializeRoundTimerPresenter();
      }
    }

    public void Exit()
    {
      gridPresenterProvider.GridPresenter.SetGridEnabled(false);
      player.OnPlayerMadeMove -= EndPlayerTurn;
      
      if (timer != null)
      {
        timer.OnTimerEnd -= EndGame;
        DisableRoundTimer();
      }
    }

    private void DisableRoundTimer()
    {
      roundTimerPresenterProvider.RoundTimerPresenter.Cleanup();
    }

    private void EndPlayerTurn()
    {
      if (drawWinService.CheckWin())
      {
        gameStateMachine.Enter<WinState, IPlayer>(player);
        return;
      }
      
      if(drawWinService.CheckDraw())
      {
        gameStateMachine.Enter<DrawState>();
        return;
      }
      
      playersOrderService.SetCurrentPlayer(playersOrderService.NextPlayer);
      playersOrderService.SetNextPlayer(player);
      
      gameStateMachine.Enter<PlayerTurnState, IPlayer>(playersOrderService.CurrentPlayer);
    }

    private void InitializeTimer()
    {
      timer = timerService.CreateTimer();
      timer.OnTimerEnd += EndGame;
      timer.StartTimer(initialGameConfig.OneRoundTime);
    }

    private void InitializeRoundTimerPresenter()
    {
      roundTimerPresenterProvider.RoundTimerPresenter.SetupTimer(timer);
      roundTimerPresenterProvider.RoundTimerPresenter.Initialize();
    }

    private bool IsHumanTurn() => 
      player is HumanPlayer;

    private void EndGame() => 
      gameStateMachine.Enter<WinState, IPlayer>(playersOrderService.NextPlayer);
  }
}