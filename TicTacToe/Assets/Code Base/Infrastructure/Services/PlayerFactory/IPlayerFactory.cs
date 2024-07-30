using Code_Base.Enums;
using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.Player;

namespace Code_Base.Infrastructure.Services.PlayerFactory
{
  public interface IPlayerFactory : IService
  {
    IPlayer CreateComputerPlayer(SignType signType, PlayerType playerType);
    IPlayer CreateHumanPlayer(SignType signType, PlayerType playerType);
  }
}