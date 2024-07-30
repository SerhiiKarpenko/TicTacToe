using System;
using Code_Base.Enums;
using Code_Base.Infrastructure.Services.CommandService;
using Code_Base.Infrastructure.Services.DrawWinService;
using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Code_Base.Infrastructure.Services.NextPlayerService;
using Code_Base.Infrastructure.Services.PlayerFactory;
using Code_Base.Infrastructure.Services.RandomService;
using Code_Base.Player;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameStateMachine.States
{
  public class GameLoopInitialState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly IGameSetupService gameSetupService;
    private readonly IPlayersOrderService playersOrderService;
    private readonly IGridPresenterProvider gridPresenterProvider;
    private readonly IPlayerFactory playerFactory;
    private readonly ICommandService commandService;
    private readonly IRandomService randomService;
    private readonly IDrawWinService drawWinService;
    
    [Inject]
    public GameLoopInitialState(
      IGameStateMachine gameStateMachine,
      IGameSetupService gameSetupService, 
      IPlayersOrderService playersOrderService, 
      IGridPresenterProvider gridPresenterProvider,
      IPlayerFactory playerFactory, 
      ICommandService commandService, 
      IRandomService randomService, 
      IDrawWinService drawWinService
      )
    {
      this.gameStateMachine = gameStateMachine;
      this.gameSetupService = gameSetupService;
      this.playersOrderService = playersOrderService;
      this.gridPresenterProvider = gridPresenterProvider;
      this.playerFactory = playerFactory;
      this.commandService = commandService;
      this.randomService = randomService;
      this.drawWinService = drawWinService;
    }
    
    public void Enter()
    {
      commandService.Dispose();
      gridPresenterProvider.GridPresenter.ClearGrid();
      gridPresenterProvider.GridPresenter.SubscribeOnCellsEvents();
      gridPresenterProvider.GridPresenter.SetGridEnabled(false);

      SetupDrawWinService();
      InitializePlayers();
      SetNextPlayer();
      
      gameStateMachine.Enter<PlayerTurnState, IPlayer>(playersOrderService.CurrentPlayer);
    }

    public void Exit() { }

    private void InitializePlayers()
    {
      switch (gameSetupService.GameMode)
      {
        case GameMode.PlayerVsPlayer:
          SetPlayers(playerFactory.CreateHumanPlayer(SignType.Cross, PlayerType.FirstPlayer), playerFactory.CreateHumanPlayer(SignType.Circle, PlayerType.SecondPlayer));
          break;
        case GameMode.PlayerVsComputer:
          SetRandomPlayers();
          break;
        case GameMode.ComputerVsComputer:
          SetPlayers(playerFactory.CreateComputerPlayer(SignType.Cross, PlayerType.FirstPlayer), playerFactory.CreateComputerPlayer(SignType.Circle, PlayerType.SecondPlayer));
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(gameSetupService.GameMode), gameSetupService.GameMode, null);
      }
    }

    private void SetupDrawWinService() => 
      drawWinService.SetupGrid(gridPresenterProvider.GridPresenter.Grid);

    private void SetRandomPlayers()
    {
      IPlayer firstPlayer;
      IPlayer secondPlayer;
      
      if (randomService.RandomFloatInRange(0, 1) > 0.5f)
      {
        firstPlayer = playerFactory.CreateHumanPlayer(SignType.Cross, PlayerType.FirstPlayer);
        secondPlayer = playerFactory.CreateComputerPlayer(SignType.Circle, PlayerType.SecondPlayer);
      }
      else
      {
        firstPlayer = playerFactory.CreateComputerPlayer(SignType.Cross, PlayerType.FirstPlayer);
        secondPlayer = playerFactory.CreateHumanPlayer(SignType.Circle, PlayerType.SecondPlayer);
      }
      
      SetPlayers(firstPlayer, secondPlayer);
    }

    private void SetPlayers(IPlayer firstPlayer, IPlayer secondPlayer)
    {
      gameSetupService.SetupPlayer(firstPlayer, PlayerType.FirstPlayer);
      gameSetupService.SetupPlayer(secondPlayer, PlayerType.SecondPlayer);
    }

    private void SetNextPlayer()
    {
      playersOrderService.SetCurrentPlayer(gameSetupService.FirstPlayer);
      playersOrderService.SetNextPlayer(gameSetupService.SecondPlayer);
    }
  }
}