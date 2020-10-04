using System;
using GameEnvironments.Common.Data.LevelDatas;
using Maps;
using WorldConfigurations;

namespace GameEnvironments.Common.Data
{
    /// <summary>
    /// Basically a game level, including all data one required to load a level(both visually and "backend" wise)
    /// todo: handle overrides with gameobjects
    /// </summary>
    public class GameEnvironment
    {
        private static readonly Lazy<GameEnvironment> LazyEmpty = new Lazy<GameEnvironment>(
            () =>
                new GameEnvironment(
                    MapConfig.Empty,
                    WorldConfig.Empty,
                    LevelData.Empty,
                    "EMPTY_ENVIRONMENT"
                )
        );

        public GameEnvironment(MapConfig mapConfiguration,
                               WorldConfig worldConfiguration,
                               LevelData levelData,
                               string environmentName)
        {
            MapConfiguration = mapConfiguration;
            WorldConfiguration = worldConfiguration;
            LevelData = levelData;
            EnvironmentName = environmentName;
        }

        public static GameEnvironment Empty => LazyEmpty.Value;

        public string EnvironmentName { get; }
        public LevelData LevelData { get; }
        public WorldConfig WorldConfiguration { get; }
        public MapConfig MapConfiguration { get; }
    }
}