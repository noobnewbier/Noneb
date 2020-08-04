using System;

namespace GameEnvironments.Common.Data
{
    //label everything with int and hence ordering in the palette matters
    //todo: add mechanism to convert to GameEnvironment
    //todo: handle overrides
    [Serializable]
    public class GameEnvironmentJson
    {
        public GameEnvironmentJson(int[] tileDatas,
                               int[] tileRepresentations,
                               int[] constructDatas,
                               int[] constructRepresentations,
                               int[] unitDatas,
                               int[] unitRepresentations,
                               float innerRadius,
                               int xSize,
                               int zSize,
                               float upAxisX,
                               float upAxisY,
                               float upAxisZ)
        {
            TileDatas = tileDatas;
            TileRepresentations = tileRepresentations;
            ConstructDatas = constructDatas;
            ConstructRepresentations = constructRepresentations;
            UnitDatas = unitDatas;
            UnitRepresentations = unitRepresentations;
            InnerRadius = innerRadius;
            XSize = xSize;
            ZSize = zSize;
            UpAxisX = upAxisX;
            UpAxisY = upAxisY;
            UpAxisZ = upAxisZ;
        }

        public int[] TileDatas { get; }
        public int[] TileRepresentations { get; }
        public int[] ConstructDatas { get; }
        public int[] ConstructRepresentations { get; }
        public int[] UnitDatas { get; }
        public int[] UnitRepresentations { get; }
        
        public float InnerRadius {get;}
        public int XSize {get;}
        public int ZSize {get;}
        public float UpAxisX {get;}
        public float UpAxisY {get;}
        public float UpAxisZ {get;}
    }
}