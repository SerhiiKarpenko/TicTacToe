using Code_Base.Infrastructure.Services.GameSetupService;
using Code_Base.Infrastructure.Services.GameStateMachine.States.StatesInterface;
using Code_Base.Infrastructure.Services.UIFactory;
using Code_Base.UI.EndGameWindow;
using Zenject;

namespace Code_Base.Infrastructure.Services.GameStateMachine.States
{
  public class DrawState : IState
  {
    private IEndGamePresenter endGamePresenter;
    
    private readonly IGameStateMachine gameStateMachine;
    private readonly IGameSetupService gameSetupService;
    private readonly IUIFactory uiFactory;
    
    [Inject]
    public DrawState(IGameStateMachine gameStateMachine, IUIFactory uiFactory, IGameSetupService gameSetupService)
    {
      this.gameStateMachine = gameStateMachine;
      this.uiFactory = uiFactory;
      this.gameSetupService = gameSetupService;
    }
    
    public void Enter()
    {
      EndGameVisual endGameMenuVisual = uiFactory.CreateEndGameMenu();
      endGamePresenter = new EndGamePresenter(endGameMenuVisual, gameStateMachine);
      
      endGamePresenter.Initialize();
      endGamePresenter.EnableDrawObject();
      
      gameSetupService.FirstPlayer.Deinitialize();
      gameSetupService.SecondPlayer.Deinitialize();
    }

    public void Exit() => 
      endGamePresenter.Cleanup();
  }
}