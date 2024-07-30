using UnityEngine;

namespace Code_Base.Infrastructure.Services.RandomService
{
  public class RandomService : IRandomService
  {
    public float RandomFloatInRange(float min, float max) => 
      Random.Range(min, max);

    public int RandomInInRange(int min, int max) => 
      Random.Range(min, max);
  }
}