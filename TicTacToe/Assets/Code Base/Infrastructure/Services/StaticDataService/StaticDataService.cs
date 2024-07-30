using System.Collections.Generic;
using System.Linq;
using Code_Base.Infrastructure.Configs.StaticDataPaths;
using Code_Base.Infrastructure.StaticData;
using UnityEngine;

namespace Code_Base.Infrastructure.Services.StaticDataService
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<string, LevelStaticData> levelDatum = new();
    
    private readonly StaticDataPaths staticDataPaths;

    public StaticDataService(StaticDataPaths staticDataPaths)
    {
      this.staticDataPaths = staticDataPaths;
    }
    
    public void LoadLevels()
    {
      levelDatum = Resources
        .LoadAll<LevelStaticData>(staticDataPaths.LevelStatiсDataPath)
        .ToDictionary(data => data.LevelKey, data => data);
    }

    public LevelStaticData ForLevel(string levelKey) => 
      levelDatum.TryGetValue(levelKey, out LevelStaticData levelStaticData)
        ? levelStaticData
        : null;
  }
}