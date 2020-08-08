using System;
using UnityEngine;

namespace GameEnvironments.Common.Data.GameEnvironments
{
    //label everything with int and hence ordering in the palette matters
    //todo: add mechanism to convert to GameEnvironment
    //todo: handle overrides
    [Serializable]
    public class GameEnvironmentJson
    {
        #region LevelData

        [SerializeField] private int[] tileDatas;
        [SerializeField] private int[] tileGameObjectProviders;
        [SerializeField] private int[] constructDatas;
        [SerializeField] private int[] constructGameObjectProviders;
        [SerializeField] private int[] unitDatas;
        [SerializeField] private int[] unitGameObjectProviders;

        #endregion

        #region MapConfiguration

        [SerializeField] private int xSize;
        [SerializeField] private int zSize;

        #endregion

        #region WorldConfig

        [SerializeField] private float innerRadius;
        [SerializeField] private Vector3 upAxis;

        #endregion

        public GameEnvironmentJson(int[] tileDatas,
                                   int[] tileGameObjectProviders,
                                   int[] constructDatas,
                                   int[] constructGameObjectProviders,
                                   int[] unitDatas,
                                   int[] unitGameObjectProviders,
                                   float innerRadius,
                                   int xSize,
                                   int zSize,
                                   Vector3 upAxis)
        {
            this.tileDatas = tileDatas;
            this.tileGameObjectProviders = tileGameObjectProviders;
            this.constructDatas = constructDatas;
            this.constructGameObjectProviders = constructGameObjectProviders;
            this.unitDatas = unitDatas;
            this.unitGameObjectProviders = unitGameObjectProviders;
            this.innerRadius = innerRadius;
            this.xSize = xSize;
            this.zSize = zSize;
            this.upAxis = upAxis;
        }

        public int[] TileDatas => tileDatas;
        public int[] TileGameObjectProviders => tileGameObjectProviders;
        public int[] ConstructDatas => constructDatas;
        public int[] ConstructGameObjectProviders => constructGameObjectProviders;
        public int[] UnitDatas => unitDatas;
        public int[] UnitGameObjectProviders => unitGameObjectProviders;
        public float InnerRadius => innerRadius;
        public int XSize => xSize;
        public int ZSize => zSize;
        public Vector3 UpAxis => upAxis;
    }
}