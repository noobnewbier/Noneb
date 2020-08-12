using GameEnvironments.Common.Data.LevelDatas;
using Maps;
using WorldConfigurations;

namespace GameEnvironments.Common.Data
{
    /// <summary>
    /// Basically a game level, including all data one required to load a level(both visually and "backend" wise)
    /// 
    /// todo: implement a fucking environment validator
    /// todo: collection better be readonly, in case you accidentally assign stuffs... or should they?
    /// todo: handle overrides
    /// </summary>
    public class GameEnvironment
    {
        public GameEnvironment(MapConfiguration mapConfiguration,
                               WorldConfiguration worldConfiguration,
                               LevelData levelData,
                               string environmentName)
        {
            MapConfiguration = mapConfiguration;
            WorldConfiguration = worldConfiguration;
            LevelData = levelData;
            EnvironmentName = environmentName;
        }

        public string EnvironmentName { get; }
        public LevelData LevelData { get; }
        public WorldConfiguration WorldConfiguration { get; }
        public MapConfiguration MapConfiguration { get; }
    }
}