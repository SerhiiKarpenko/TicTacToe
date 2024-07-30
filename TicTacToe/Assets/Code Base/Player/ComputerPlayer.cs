using Code_Base.Enums;
using Code_Base.Infrastructure.Services.TimerService;
using Code_Base.TicTacToeGrid;

namespace Code_Base.Player
{
  public class ComputerPlayer : Player
  {
    private readonly float thinkTime;
    private readonly ITimerService timerService;
    private ITimer timer;

    public ComputerPlayer(SignType signType, PlayerType playerType,IGridPresenter gridPresenter, float thinkTime, ITimerService timerService)
      : base(signType, playerType,gridPresenter)
    {
      this.thinkTime = thinkTime;
      this.timerService = timerService;
    }

    public override PlayerIdentifier PlayerIdentifier() => 
      Enums.PlayerIdentifier.Computer;

    public override void StartMove()
    {
      gridPresenter.OnGridCellOccupied += EndMove;

      timer = timerService.CreateTimer();
      timer.OnTimerEnd += MakeMove;
      timer.StartTimer(thinkTime);
    }
    
    public override void Deinitialize()
    {
      gridPresenter.OnGridCellOccupied -= EndMove;
      
      if (timer != null)
      {
        timer.OnTimerEnd -= MakeMove;
      }
    }

    protected override void EndMove()
    {
      Deinitialize();
      base.EndMove();
    }

    private void MakeMove()
    {
      if (gridPresenter.NoFreeCells())
      {
        base.EndMove();
        return;
      }

      GridCellPresenter gridCellPresenter = gridPresenter.GetRandomNonOccupiedGridCellPresenter();
      gridCellPresenter.MakeMoveCommand();
    }
  }
}