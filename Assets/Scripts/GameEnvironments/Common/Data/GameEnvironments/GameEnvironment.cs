using Common.Providers;
using Constructs;
using GameEnvironments.Common.Data.LevelDatas;
using Maps;
using Strongholds;
using Tiles.Data;
using Units;
using WorldConfigurations;

namespace GameEnvironments.Common.Data.GameEnvironments
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
                               LevelData levelData)
        {
            MapConfiguration = mapConfiguration;
            WorldConfiguration = worldConfiguration;
            LevelData = levelData;
        }

        public LevelData LevelData { get; }
        public WorldConfiguration WorldConfiguration { get; }
        public MapConfiguration MapConfiguration { get; }
    }
}