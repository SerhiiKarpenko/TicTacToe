using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.UIFactory;
using Code_Base.Player;
using Code_Base.UI.EndGameWindow;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameStateMachine.States
{
  public class WinState : IPayloadState<IPlayer>
  {
    private IEndGamePresenter endGamePresenter;
    
    private readonly IGameStateMachine gameStateMachine;
    private readonly IGameSetupService gameSetupService;
    private readonly IUIFactory uiFactory;
    
    [Inject]
    public WinState(IUIFactory uiFactory, IGameStateMachine gameStateMachine, IGameSetupService gameSetupService)
    {
      this.uiFactory = uiFactory;
      this.gameStateMachine = gameStateMachine;
      this.gameSetupService = gameSetupService;
    }

    public void Enter(IPlayer player)
    {
      CreateEndGameMenu();
      ShowWinner(player);

      gameSetupService.FirstPlayer.Deinitialize();
      gameSetupService.SecondPlayer.Deinitialize();
    }

    public void Exit() => 
      endGamePresenter.Cleanup();

    private void CreateEndGameMenu()
    {
      EndGameVisual endGameMenuVisual = uiFactory.CreateEndGameMenu();
      endGamePresenter = new EndGamePresenter(endGameMenuVisual, gameStateMachine);
      endGamePresenter.Initialize();
    }

    private void ShowWinner(IPlayer player) => 
      endGamePresenter.EnableWinObject(player);
  }
  
}