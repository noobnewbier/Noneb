using Common.Providers;
using Constructs;
using Strongholds;
using Tiles.Data;
using Units;

namespace GameEnvironments.Common.Data.LevelDatas
{
    /// <summary>
    /// Basically a game level, including all data one required to load a level(both visually and "backend" wise)
    /// todo: implement a fucking environment validator
    /// todo: collection better be readonly, in case you accidentally assign stuffs... or should they?
    /// todo: handle overrides
    /// </summary>
    public class LevelData
    {
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