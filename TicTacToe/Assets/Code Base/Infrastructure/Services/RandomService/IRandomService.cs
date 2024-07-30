using Code_Base.Infrastructure.Services.ServiceInterfaces;

namespace Code_Base.Infrastructure.Services.RandomService
{
  public interface IRandomService : IService
  {
    public float RandomFloatInRange(float min, float max);
    public int RandomInInRange(int min, int max);
  }
}