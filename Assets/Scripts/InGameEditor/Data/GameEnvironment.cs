using Constructs;
using Maps;
using Tiles.Data;
using Tiles.Representation;
using Units;
using Units.Representation;

namespace InGameEditor.Data
{
    //todo: handle overrides
    public class GameEnvironment
    {
        public MapConfiguration MapConfiguration { get; }
        public TileData[] TileDatas { get; }
        public TileRepresentationProvider[] TileRepresentationProviders { get; }
        public ConstructData[] ConstructDatas { get; }
        public ConstructRepresentationProvider[] ConstructRepresentationProviders { get; }
        public UnitData[] UnitDatas { get; }
        public UnitRepresentationProvider[] UnitRepresentationProviders { get; }

        public GameEnvironment(TileRepresentationProvider[] tileRepresentationProviders, ConstructData[] constructDatas, ConstructRepresentationProvider[] constructRepresentationProviders, UnitData[] unitDatas, UnitRepresentationProvider[] unitRepresentationProviders, TileData[] tileDatas, MapConfiguration mapConfiguration)
        {
            TileRepresentationProviders = tileRepresentationProviders;
            ConstructDatas = constructDatas;
            ConstructRepresentationProviders = constructRepresentationProviders;
            UnitDatas = unitDatas;
            UnitRepresentationProviders = unitRepresentationProviders;
            TileDatas = tileDatas;
            MapConfiguration = mapConfiguration;
        }
    }
}