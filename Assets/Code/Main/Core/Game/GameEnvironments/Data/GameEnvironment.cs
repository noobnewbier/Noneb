using System;
using Main.Core.Game.GameEnvironments.Data.LevelDatas;
using Main.Core.Game.Maps;
using Main.Core.Game.WorldConfigurations;

namespace Main.Core.Game.GameEnvironments.Data
{
    /// <summary>
    /// Basically a game level, including all data one required to load a level(both visually and "backend" wise)
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