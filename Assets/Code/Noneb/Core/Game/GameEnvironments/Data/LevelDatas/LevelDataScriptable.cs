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
        [FormerlySerializedAs("tileGameObjectProviders")] [SerializeField] private GameObjectFactory[] tileGameObjectFactories;
        [SerializeField] private ConstructDataScriptable[] constructDatas;
        [FormerlySerializedAs("constructGameObjectProviders")] [SerializeField] private GameObjectFactory[] constructGameObjectFactories;
        [SerializeField] private UnitDataScriptable[] unitDatas;
        [FormerlySerializedAs("unitGameObjectProviders")] [SerializeField] private GameObjectFactory[] unitGameObjectFactories;
        [SerializeField] private StrongholdDataWrapper[] strongholdDatas;
        [FormerlySerializedAs("strongholdUnitGameObjectProviders")] [SerializeField] private GameObjectFactory[] strongholdUnitGameObjectFactories;
        [FormerlySerializedAs("strongholdConstructGameObjectProviders")] [SerializeField] private GameObjectFactory[] strongholdConstructGameObjectFactories;

#if UNITY_EDITOR
        [TextArea] [SerializeField] [UsedImplicitly]
        private string devInformation;
#endif

        public StrongholdDataWrapper[] StrongholdDatas => strongholdDatas;
        public TileDataScriptable[] TileDatas => tileDatas;
        public GameObjectFactory[] TileGameObjectFactories => tileGameObjectFactories;
        public ConstructDataScriptable[] ConstructDatas => constructDatas;
        public GameObjectFactory[] ConstructGameObjectFactories => constructGameObjectFactories;
        public UnitDataScriptable[] UnitDatas => unitDatas;
        public GameObjectFactory[] UnitGameObjectFactories => unitGameObjectFactories;
        public GameObjectFactory[] StrongholdUnitGameObjectFactories => strongholdUnitGameObjectFactories;
        public GameObjectFactory[] StrongholdConstructGameObjectFactories => strongholdConstructGameObjectFactories;

        public LevelData ToLevelData()
        {
            return new LevelData(
                tileDatas.Select(d => d != null ? d.ToData() : null).ToArray(),
                (GameObjectFactory[]) tileGameObjectFactories.Clone(),
                constructDatas.Select(d => d != null ? d.ToData() : null).ToArray(),
                (GameObjectFactory[]) constructGameObjectFactories.Clone(),
                unitDatas.Select(d => d != null ? d.ToData() : null).ToArray(),
                (GameObjectFactory[]) unitGameObjectFactories.Clone(),
                strongholdDatas.Select(wrapper => wrapper.ToStrongholdData()).ToArray(),
                (GameObjectFactory[]) strongholdUnitGameObjectFactories.Clone(),
                (GameObjectFactory[]) strongholdConstructGameObjectFactories.Clone()
            );
        }

        public static LevelDataScriptable Create(LevelData levelData)
        {
            var newInstance = CreateInstance<LevelDataScriptable>();
            newInstance.tileDatas = levelData.TileDatas.Select(d => d?.Original).ToArray();
            newInstance.tileGameObjectFactories = (GameObjectFactory[]) levelData.TileGameObjectFactories.Clone();
            newInstance.constructDatas = levelData.ConstructDatas.Select(d => d?.Original).ToArray();
            newInstance.constructGameObjectFactories = (GameObjectFactory[]) levelData.ConstructGameObjectFactories.Clone();
            newInstance.unitDatas = levelData.UnitDatas.Select(d => d?.Original).ToArray();
            newInstance.unitGameObjectFactories = (GameObjectFactory[]) levelData.UnitGameObjectFactories.Clone();
            newInstance.strongholdDatas = levelData.StrongholdDatas
                .Select(
                    data => data != null ?
                        new StrongholdDataWrapper(data.UnitData.Original, data.ConstructData.Original) :
                        new StrongholdDataWrapper(null, null)
                )
                .ToArray();
            newInstance.strongholdUnitGameObjectFactories = (GameObjectFactory[]) levelData.StrongholdUnitGameObjectFactories.Clone();
            newInstance.strongholdConstructGameObjectFactories = (GameObjectFactory[]) levelData.StrongholdConstructGameObjectFactories.Clone();

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