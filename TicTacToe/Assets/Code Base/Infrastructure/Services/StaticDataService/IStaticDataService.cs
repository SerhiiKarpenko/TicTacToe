using Code_Base.Infrastructure.Services.ServiceInterfaces;
using Code_Base.Infrastructure.StaticData;

namespace Code_Base.Infrastructure.Services.StaticDataService
{
  public interface IStaticDataService : IService
  {
    public void LoadLevels();
    public LevelStaticData ForLevel(string levelKey);
  }
}