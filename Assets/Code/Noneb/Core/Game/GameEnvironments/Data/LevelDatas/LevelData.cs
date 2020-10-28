using System;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Tiles;
using Noneb.Core.Game.Units;

namespace Noneb.Core.Game.GameEnvironments.Data.LevelDatas
{
    /// <summary>
    /// Basically a game level, including all data one required to load a level(both visually and "backend" wise)
    /// </summary>
    public class LevelData
    {
        private static readonly Lazy<LevelData> LazyEmpty = new Lazy<LevelData>(
            () => new LevelData(
                new TileData[0],
                new GameObjectFactory[0],
                new ConstructData[0],
                new GameObjectFactory[0],
                new UnitData[0],
                new GameObjectFactory[0],
                new StrongholdData[0],
                new GameObjectFactory[0],
                new GameObjectFactory[0]
            )
        );

        public LevelData(TileData[] tileDatas,
                         GameObjectFactory[] tileGameObjectFactories,
                         ConstructData[] constructDatas,
                         GameObjectFactory[] constructGameObjectFactories,
                         UnitData[] unitDatas,
                         GameObjectFactory[] unitGameObjectFactories,
                         StrongholdData[] strongholdDatas,
                         GameObjectFactory[] strongholdUnitGameObjectFactories,
                         GameObjectFactory[] strongholdConstructGameObjectFactories)
        {
            TileDatas = tileDatas;
            TileGameObjectFactories = tileGameObjectFactories;
            ConstructDatas = constructDatas;
            ConstructGameObjectFactories = constructGameObjectFactories;
            UnitDatas = unitDatas;
            UnitGameObjectFactories = unitGameObjectFactories;
            StrongholdUnitGameObjectFactories = strongholdUnitGameObjectFactories;
            StrongholdConstructGameObjectFactories = strongholdConstructGameObjectFactories;
            StrongholdDatas = strongholdDatas;
        }

        public static LevelData Empty => LazyEmpty.Value;

        public TileData[] TileDatas { get; }
        public GameObjectFactory[] TileGameObjectFactories { get; }
        public ConstructData[] ConstructDatas { get; }
        public GameObjectFactory[] ConstructGameObjectFactories { get; }
        public UnitData[] UnitDatas { get; }
        public GameObjectFactory[] UnitGameObjectFactories { get; }
        public StrongholdData[] StrongholdDatas { get; }
        public GameObjectFactory[] StrongholdUnitGameObjectFactories { get; }
        public GameObjectFactory[] StrongholdConstructGameObjectFactories { get; }
    }
}