using System;
using System.Linq;
using JetBrains.Annotations;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Tiles;
using Noneb.Core.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameEnvironments.Data.LevelDatas
{
    [CreateAssetMenu(menuName = MenuName.Data + nameof(LevelDataScriptable), fileName = nameof(LevelDataScriptable))]
    public class LevelDataScriptable : ScriptableObject
    {
        [SerializeField] private TileDataScriptable[] tileDatas;
        [SerializeField] private GameObjectFactory[] tileGameObjectProviders;
        [SerializeField] private ConstructDataScriptable[] constructDatas;
        [SerializeField] private GameObjectFactory[] constructGameObjectProviders;
        [SerializeField] private UnitDataScriptable[] unitDatas;
        [SerializeField] private GameObjectFactory[] unitGameObjectProviders;
        [SerializeField] private StrongholdDataWrapper[] strongholdDatas;
        [SerializeField] private GameObjectFactory[] strongholdUnitGameObjectProviders;
        [SerializeField] private GameObjectFactory[] strongholdConstructGameObjectProviders;

#if UNITY_EDITOR
        [TextArea] [SerializeField] [UsedImplicitly]
        private string devInformation;
#endif

        public StrongholdDataWrapper[] StrongholdDatas => strongholdDatas;
        public TileDataScriptable[] TileDatas => tileDatas;
        public GameObjectFactory[] TileGameObjectProviders => tileGameObjectProviders;
        public ConstructDataScriptable[] ConstructDatas => constructDatas;
        public GameObjectFactory[] ConstructGameObjectProviders => constructGameObjectProviders;
        public UnitDataScriptable[] UnitDatas => unitDatas;
        public GameObjectFactory[] UnitGameObjectProviders => unitGameObjectProviders;
        public GameObjectFactory[] StrongholdUnitGameObjectProviders => strongholdUnitGameObjectProviders;
        public GameObjectFactory[] StrongholdConstructGameObjectProviders => strongholdConstructGameObjectProviders;

        public LevelData ToLevelData()
        {
            return new LevelData(
                tileDatas.Select(d => d != null ? d.ToData() : null).ToArray(),
                tileGameObjectProviders,
                constructDatas.Select(d => d != null ? d.ToData() : null).ToArray(),
                constructGameObjectProviders,
                unitDatas.Select(d => d != null ? d.ToData() : null).ToArray(),
                unitGameObjectProviders,
                strongholdDatas.Select(wrapper => wrapper.ToStrongholdData()).ToArray(),
                strongholdUnitGameObjectProviders,
                strongholdConstructGameObjectProviders
            );
        }

        public static LevelDataScriptable Create(LevelData levelData)
        {
            var newInstance = CreateInstance<LevelDataScriptable>();
            newInstance.tileDatas = levelData.TileDatas.Select(d => d?.Original).ToArray();
            newInstance.tileGameObjectProviders = levelData.TileGameObjectProviders;
            newInstance.constructDatas = levelData.ConstructDatas.Select(d => d?.Original).ToArray();
            newInstance.constructGameObjectProviders = levelData.ConstructGameObjectProviders;
            newInstance.unitDatas = levelData.UnitDatas.Select(d => d?.Original).ToArray();
            newInstance.unitGameObjectProviders = levelData.UnitGameObjectProviders;
            newInstance.strongholdDatas = levelData.StrongholdDatas
                .Select(
                    data => data != null ?
                        new StrongholdDataWrapper(data.UnitData.Original, data.ConstructData.Original) :
                        new StrongholdDataWrapper(null, null)
                )
                .ToArray();
            newInstance.strongholdUnitGameObjectProviders = levelData.StrongholdUnitGameObjectProviders;
            newInstance.strongholdConstructGameObjectProviders = levelData.StrongholdConstructGameObjectProviders;

            return newInstance;
        }

        /// <summary>
        /// Required to avoid passing in empty but not null object into the environment
        /// </summary>
        [Serializable]
        public class StrongholdDataWrapper
        {
            [FormerlySerializedAs("unitData")] [SerializeField]
            private UnitDataScriptable unitDataScriptable;

            [SerializeField] private ConstructDataScriptable constructDataScriptable;

            public StrongholdDataWrapper(UnitDataScriptable unitDataScriptable, ConstructDataScriptable constructDataScriptable)
            {
                this.unitDataScriptable = unitDataScriptable;
                this.constructDataScriptable = constructDataScriptable;
            }

            public UnitDataScriptable UnitDataScriptable => unitDataScriptable;
            public ConstructDataScriptable ConstructDataScriptable => constructDataScriptable;

            [CanBeNull]
            public StrongholdData ToStrongholdData()
            {
                if (unitDataScriptable == null && constructDataScriptable == null) return null;

                return StrongholdData.Create(constructDataScriptable.ToData(), unitDataScriptable.ToData());
            }
        }
    }
}