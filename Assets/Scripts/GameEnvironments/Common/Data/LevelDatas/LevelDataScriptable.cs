using System;
using System.Linq;
using Common.Providers;
using Constructs.Data;
using JetBrains.Annotations;
using Strongholds;
using Tiles.Data;
using Units.Data;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Data.LevelDatas
{
    [CreateAssetMenu(menuName = MenuName.Data + nameof(LevelDataScriptable), fileName = nameof(LevelDataScriptable))]
    public class LevelDataScriptable : ScriptableObject
    {
        [SerializeField] private TileDataScriptable[] tileDatas;
        [SerializeField] private GameObjectProvider[] tileGameObjectProviders;
        [SerializeField] private ConstructDataScriptable[] constructDatas;
        [SerializeField] private GameObjectProvider[] constructGameObjectProviders;
        [SerializeField] private UnitDataScriptable[] unitDatas;
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
                tileDatas.Select(d => d.ToData()).ToArray(),
                tileGameObjectProviders,
                constructDatas.Select(d => d.ToData()).ToArray(),
                constructGameObjectProviders,
                unitDatas.Select(d => d.ToData()).ToArray(),
                unitGameObjectProviders,
                strongholdDatas.Select(wrapper => wrapper.ToStrongholdData()).ToArray(),
                strongholdUnitGameObjectProviders,
                strongholdConstructGameObjectProviders
            );
        }

        public static LevelDataScriptable Create(LevelData levelData)
        {
            var newInstance = CreateInstance<LevelDataScriptable>();
            newInstance.tileDatas = levelData.TileDatas.Select(d => d.Original).ToArray();
            newInstance.tileGameObjectProviders = levelData.TileGameObjectProviders;
            newInstance.constructDatas = levelData.ConstructDatas.Select(d => d.Original).ToArray();
            newInstance.constructGameObjectProviders = levelData.ConstructGameObjectProviders;
            newInstance.unitDatas = levelData.UnitDatas.Select(d => d.Original).ToArray();
            newInstance.unitGameObjectProviders = levelData.UnitGameObjectProviders;
            newInstance.strongholdDatas = levelData.StrongholdDatas
                .Select(data => data != null ? new StrongholdDataWrapper(data.UnitData.Original, data.ConstructData.Original) : null)
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
            [FormerlySerializedAs("unitData")] [SerializeField]
            private UnitDataScriptable unitDataScriptable;

            [SerializeField] private ConstructDataScriptable constructDataScriptable;

            public StrongholdDataWrapper(UnitDataScriptable unitDataScriptable, ConstructDataScriptable constructDataScriptable)
            {
                this.unitDataScriptable = unitDataScriptable;
                this.constructDataScriptable = constructDataScriptable;
            }

            public StrongholdData ToStrongholdData()
            {
                if (unitDataScriptable == null && constructDataScriptable == null)
                {
                    return null;
                }

                return StrongholdData.Create(constructDataScriptable.ToData(), unitDataScriptable.ToData());
            }
        }
    }
}