using System;
using Common.Providers;
using Constructs.Data;
using Strongholds;
using Tiles.Data;
using Units.Data;

namespace GameEnvironments.Common.Data.LevelDatas
{
    /// <summary>
    /// Basically a game level, including all data one required to load a level(both visually and "backend" wise)
    /// </summary>
    public class LevelData
    {
        private static readonly Lazy<LevelData> LazyEmpty = new Lazy<LevelData>(
            () => new LevelData(
                new TileData[0],
                new GameObjectProvider[0],
                new ConstructData[0],
                new GameObjectProvider[0],
                new UnitData[0],
                new GameObjectProvider[0],
                new StrongholdData[0],
                new GameObjectProvider[0],
                new GameObjectProvider[0]
            )
        );

        public LevelData(TileData[] tileDatas,
                         GameObjectProvider[] tileGameObjectProviders,
                         ConstructData[] constructDatas,
                         GameObjectProvider[] constructGameObjectProviders,
                         UnitData[] unitDatas,
                         GameObjectProvider[] unitGameObjectProviders,
                         StrongholdData[] strongholdDatas,
                         GameObjectProvider[] strongholdUnitGameObjectProviders,
                         GameObjectProvider[] strongholdConstructGameObjectProviders)
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
        public GameObjectProvider[] TileGameObjectProviders { get; }
        public ConstructData[] ConstructDatas { get; }
        public GameObjectProvider[] ConstructGameObjectProviders { get; }
        public UnitData[] UnitDatas { get; }
        public GameObjectProvider[] UnitGameObjectProviders { get; }
        public StrongholdData[] StrongholdDatas { get; }
        public GameObjectProvider[] StrongholdUnitGameObjectProviders { get; }
        public GameObjectProvider[] StrongholdConstructGameObjectProviders { get; }
    }
}