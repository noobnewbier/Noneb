using System;
using System.Linq;
using Common.Providers;
using Constructs;
using JetBrains.Annotations;
using Strongholds;
using Tiles.Data;
using Units;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Data.LevelDatas
{
    [CreateAssetMenu(menuName = MenuName.Data + nameof(LevelDataScriptable), fileName = nameof(LevelDataScriptable))]
    public class LevelDataScriptable : ScriptableObject
    {
        [SerializeField] private TileData[] tileDatas;
        [SerializeField] private GameObjectProvider[] tileGameObjectProviders;
        [SerializeField] private ConstructData[] constructDatas;
        [SerializeField] private GameObjectProvider[] constructGameObjectProviders;
        [SerializeField] private UnitData[] unitDatas;
        [SerializeField] private GameObjectProvider[] unitGameObjectProviders;
        [SerializeField] private StrongholdDataWrapper[] strongholdDatas;
        [SerializeField] private GameObjectProvider[] strongholdUnitGameObjectProviders;
        [SerializeField] private GameObjectProvider[] strongholdConstructGameObjectProviders;

#if UNITY_EDITOR
        [TextArea] [SerializeField] [UsedImplicitly]
        private string devInformation;
#endif

        public LevelData ToLevelData()
        {
            return new LevelData(
                tileDatas,
                tileGameObjectProviders,
                constructDatas,
                constructGameObjectProviders,
                unitDatas,
                unitGameObjectProviders,
                strongholdDatas.Select(wrapper => wrapper.ToStrongholdData()).ToArray(),
                strongholdUnitGameObjectProviders,
                strongholdConstructGameObjectProviders
            );
        }

        public static LevelDataScriptable CreateScriptableFromLevelData(LevelData levelData)
        {
            var newInstance = CreateInstance<LevelDataScriptable>();
            newInstance.tileDatas = levelData.TileDatas;
            newInstance.tileGameObjectProviders = levelData.TileGameObjectProviders;
            newInstance.constructDatas = levelData.ConstructDatas;
            newInstance.constructGameObjectProviders = levelData.ConstructGameObjectProviders;
            newInstance.unitDatas = levelData.UnitDatas;
            newInstance.unitGameObjectProviders = levelData.UnitGameObjectProviders;
            newInstance.strongholdDatas = levelData.StrongholdDatas
                .Select(data => data != null ? new StrongholdDataWrapper(data.UnitData, data.ConstructData) : null)
                .ToArray();
            newInstance.strongholdUnitGameObjectProviders = levelData.StrongholdUnitGameObjectProviders;
            newInstance.strongholdConstructGameObjectProviders = levelData.StrongholdConstructGameObjectProviders;

            return newInstance;
        }

        /// <summary>
        /// Required to avoid passing in empty but not null object into the environment
        /// </summary>
        [Serializable]
        private class StrongholdDataWrapper
        {
            [SerializeField] private UnitData unitData;
            [SerializeField] private ConstructData constructData;

            public StrongholdDataWrapper(UnitData unitData, ConstructData constructData)
            {
                this.unitData = unitData;
                this.constructData = constructData;
            }

            public StrongholdData ToStrongholdData()
            {
                if (unitData == null && constructData == null)
                {
                    return null;
                }

                return new StrongholdData(unitData, constructData);
            }
        }
    }
}