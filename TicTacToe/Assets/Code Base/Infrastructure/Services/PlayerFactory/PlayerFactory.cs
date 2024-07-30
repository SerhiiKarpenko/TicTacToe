using Code_Base.Enums;
using Code_Base.Infrastructure.Configs.InitialGameConfig;
using Code_Base.Infrastructure.Services.GridPresenterProvider;
using Code_Base.Infrastructure.Services.TimerService;
using Code_Base.Player;
using Zenject;

namespace Code_Base.Infrastructure.Services.PlayerFactory
{
  public class PlayerFactory : IPlayerFactory
  {
    private readonly ITimerService timerService;
    private readonly IGridPresenterProvider gridPresenterProvider;
    private readonly InitialGameConfig initialGameConfig;
    
    [Inject]
    public PlayerFactory(ITimerService timerService, InitialGameConfig initialGameConfig, IGridPresenterProvider gridPresenterProvider)
    {
      this.timerService = timerService;
      this.initialGameConfig = initialGameConfig;
      this.gridPresenterProvider = gridPresenterProvider;
    }
    
    public IPlayer CreateComputerPlayer(SignType signType, PlayerType playerType) => 
      new ComputerPlayer(signType, playerType, gridPresenterProvider.GridPresenter, initialGameConfig.ComputerThinkTime, timerService);

    public IPlayer CreateHumanPlayer(SignType signType, PlayerType playerType) => 
      new HumanPlayer(signType, playerType, gridPresenterProvider.GridPresenter);
  }
}