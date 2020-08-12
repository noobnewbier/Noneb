using System;
using UnityEngine;

namespace ObsoleteJsonRelated
{
    [Serializable]
    public class LevelDataJson
    {
        [SerializeField] private int[] tileDatas;
        [SerializeField] private int[] tileGameObjectProviders;
        [SerializeField] private int[] constructDatas;
        [SerializeField] private int[] constructGameObjectProviders;
        [SerializeField] private int[] unitDatas;
        [SerializeField] private int[] unitGameObjectProviders;
        [SerializeField] private int[] strongholdUnitDatas;
        [SerializeField] private int[] strongholdUnitGameObjectProviders;
        [SerializeField] private int[] strongholdConstructDatas;
        [SerializeField] private int[] strongholdConstructGameObjectProviders;

        public LevelDataJson(int[] tileDatas,
                             int[] tileGameObjectProviders,
                             int[] constructDatas,
                             int[] constructGameObjectProviders,
                             int[] unitDatas,
                             int[] unitGameObjectProviders,
                             int[] strongholdUnitDatas,
                             int[] strongholdUnitGameObjectProviders,
                             int[] strongholdConstructDatas,
                             int[] strongholdConstructGameObjectProviders)
        {
            this.tileDatas = tileDatas;
            this.tileGameObjectProviders = tileGameObjectProviders;
            this.constructDatas = constructDatas;
            this.constructGameObjectProviders = constructGameObjectProviders;
            this.unitDatas = unitDatas;
            this.unitGameObjectProviders = unitGameObjectProviders;
            this.strongholdUnitDatas = strongholdUnitDatas;
            this.strongholdUnitGameObjectProviders = strongholdUnitGameObjectProviders;
            this.strongholdConstructDatas = strongholdConstructDatas;
            this.strongholdConstructGameObjectProviders = strongholdConstructGameObjectProviders;
        }

        public int[] StrongholdConstructDatas => strongholdConstructDatas;

        public int[] TileDatas => tileDatas;

        public int[] TileGameObjectProviders => tileGameObjectProviders;

        public int[] ConstructDatas => constructDatas;

        public int[] ConstructGameObjectProviders => constructGameObjectProviders;

        public int[] UnitDatas => unitDatas;

        public int[] UnitGameObjectProviders => unitGameObjectProviders;

        public int[] StrongholdUnitDatas => strongholdUnitDatas;

        public int[] StrongholdUnitGameObjectProviders => strongholdUnitGameObjectProviders;

        public int[] StrongholdConstructGameObjectProviders => strongholdConstructGameObjectProviders;
    }
}