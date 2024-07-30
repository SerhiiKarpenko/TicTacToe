using Code_Base.Enums;
using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.Player;

namespace Code_Base.Infrastructure.Services.GameSetupService
{
  public interface IGameSetupService : IService
  {
    public GameMode GameMode { get; }
    public IPlayer FirstPlayer { get; }
    public IPlayer SecondPlayer{ get; }
    
    public void SetupGameMode(GameMode gameMode);
    public void SetupPlayer(IPlayer player, PlayerType playerType);
  }
}