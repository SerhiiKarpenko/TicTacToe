using System;
using Code_Base.Enums;
using Code_Base.Player;

namespace Code_Base.Infrastructure.Services.GameSetupService
{
  public class GameSetupService : IGameSetupService
  {
    public GameMode GameMode { get; private set; }
    public IPlayer FirstPlayer { get; private set; }
    public IPlayer SecondPlayer{ get; private set; }


    public void SetupGameMode(GameMode gameMode) => 
      GameMode = gameMode;

    public void SetupPlayer(IPlayer player, PlayerType playerType)
    {
      switch (playerType)
      {
        case PlayerType.FirstPlayer:
          FirstPlayer = player;
          break;
        case PlayerType.SecondPlayer:
          SecondPlayer = player;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}