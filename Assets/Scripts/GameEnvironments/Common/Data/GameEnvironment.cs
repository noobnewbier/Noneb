using Common.Providers;
using Constructs;
using Maps;
using Tiles.Data;
using Units;

namespace GameEnvironments.Common.Data
{
    
    /// <summary>
    /// Basically a game level, including all data one required to load a level(both visually and "backend" wise)
    /// </summary>
    //todo: handle overrides
    public class GameEnvironment
    {
        public GameEnvironment(GameObjectProvider[] tileGameObjectProviders, 
                               ConstructData[] constructDatas,
                               GameObjectProvider[] constructGameObjectProviders,
                               UnitData[] unitDatas,
                               GameObjectProvider[] unitGameObjectProviders,
                               TileData[] tileDatas,
                               MapConfiguration mapConfiguration)
        {
            TileGameObjectProviders = tileGameObjectProviders;
            ConstructDatas = constructDatas;
            ConstructGameObjectProviders = constructGameObjectProviders;
            UnitDatas = unitDatas;
            UnitGameObjectProviders = unitGameObjectProviders;
            TileDatas = tileDatas;
            MapConfiguration = mapConfiguration;
        }

        public MapConfiguration MapConfiguration { get; }
        public TileData[] TileDatas { get; }
        public GameObjectProvider[] TileGameObjectProviders { get; }
        public ConstructData[] ConstructDatas { get; }
        public GameObjectProvider[] ConstructGameObjectProviders { get; }
        public UnitData[] UnitDatas { get; }
        public GameObjectProvider[] UnitGameObjectProviders { get; }
    }
}