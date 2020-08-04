using Common.Providers;
using Constructs;
using Maps;
using Strongholds;
using Tiles.Data;
using Units;

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
                               TileData[] tileDatas,
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
            MapConfiguration = mapConfiguration;
            StrongholdUnitGameObjectProviders = strongholdUnitGameObjectProviders;
            StrongholdConstructGameObjectProviders = strongholdConstructGameObjectProviders;
            StrongholdDatas = strongholdDatas;
        }

        public MapConfiguration MapConfiguration { get; }
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