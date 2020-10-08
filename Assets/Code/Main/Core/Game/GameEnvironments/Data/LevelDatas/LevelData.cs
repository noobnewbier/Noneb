using System;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Constructs;
using Main.Core.Game.Strongholds;
using Main.Core.Game.Tiles;
using Main.Core.Game.Units;

namespace Main.Core.Game.GameEnvironments.Data.LevelDatas
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
                         GameObjectFactory[] tileGameObjectProviders,
                         ConstructData[] constructDatas,
                         GameObjectFactory[] constructGameObjectProviders,
                         UnitData[] unitDatas,
                         GameObjectFactory[] unitGameObjectProviders,
                         StrongholdData[] strongholdDatas,
                         GameObjectFactory[] strongholdUnitGameObjectProviders,
                         GameObjectFactory[] strongholdConstructGameObjectProviders)
        {
            TileDatas = tileDatas;
            TileGameObjectProviders = tileGameObjectProviders;
            ConstructDatas = constructDatas;
            ConstructGameObjectProviders = constructGameObjectProviders;
            UnitDatas = unitDatas;
            UnitGameObjectProviders = unitGameObjectProviders;
            StrongholdUnitGameObjectProviders = strongholdUnitGameObjectProviders;
            StrongholdConstructGameObjectProviders = strongholdConstructGameObjectProviders;
            StrongholdDatas = strongholdDatas;
        }

        public static LevelData Empty => LazyEmpty.Value;

        public TileData[] TileDatas { get; }
        public GameObjectFactory[] TileGameObjectProviders { get; }
        public ConstructData[] ConstructDatas { get; }
        public GameObjectFactory[] ConstructGameObjectProviders { get; }
        public UnitData[] UnitDatas { get; }
        public GameObjectFactory[] UnitGameObjectProviders { get; }
        public StrongholdData[] StrongholdDatas { get; }
        public GameObjectFactory[] StrongholdUnitGameObjectProviders { get; }
        public GameObjectFactory[] StrongholdConstructGameObjectProviders { get; }
    }
}