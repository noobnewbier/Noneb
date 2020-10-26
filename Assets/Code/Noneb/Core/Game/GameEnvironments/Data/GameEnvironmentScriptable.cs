using System;
using JetBrains.Annotations;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameEnvironments.Data.LevelDatas;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Units;
using Noneb.Core.Game.WorldConfigurations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Core.Game.GameEnvironments.Data
{
    [CreateAssetMenu(menuName = "Data/GameEnvironment", fileName = nameof(GameEnvironmentScriptable))]
    public class GameEnvironmentScriptable : ScriptableObject
    {
        [SerializeField] private string environmentName;
        [SerializeField] private MapConfig mapConfiguration;

        [SerializeField] private WorldConfig worldConfiguration;
        [SerializeField] private LevelDataScriptable levelData;


#if UNITY_EDITOR
        [TextArea] [SerializeField] [UsedImplicitly]
        private string devInformation;
#endif

        public string EnvironmentName => environmentName;

        public MapConfig MapConfiguration => mapConfiguration;

        public WorldConfig WorldConfiguration => worldConfiguration;

        public LevelDataScriptable LevelData => levelData;

        public GameEnvironment ToGameEnvironment() =>
            new GameEnvironment(
                mapConfiguration,
                worldConfiguration,
                levelData.ToLevelData(),
                environmentName
            );

        public static GameEnvironmentScriptable Create(string environmentName,
                                                       MapConfig mapConfiguration,
                                                       WorldConfig worldConfiguration,
                                                       LevelDataScriptable levelDataScriptable)
        {
            var newInstance = CreateInstance<GameEnvironmentScriptable>();

            newInstance.environmentName = environmentName;
            newInstance.mapConfiguration = mapConfiguration;
            newInstance.worldConfiguration = worldConfiguration;
            newInstance.levelData = levelDataScriptable;

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

            [FormerlySerializedAs("constructData")] [SerializeField]
            private ConstructDataScriptable constructDataScriptable;

            public StrongholdDataWrapper(UnitDataScriptable unitDataScriptable, ConstructDataScriptable constructDataScriptable)
            {
                this.unitDataScriptable = unitDataScriptable;
                this.constructDataScriptable = constructDataScriptable;
            }

            public StrongholdData ToStrongholdData()
            {
                if (unitDataScriptable == null && constructDataScriptable == null) return null;

                return StrongholdData.Create(constructDataScriptable.ToData(), unitDataScriptable.ToData());
            }
        }
    }
}