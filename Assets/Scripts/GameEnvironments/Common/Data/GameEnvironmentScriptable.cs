using System;
using Constructs;
using GameEnvironments.Common.Data.LevelDatas;
using JetBrains.Annotations;
using Maps;
using Strongholds;
using Units;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using WorldConfigurations;

namespace GameEnvironments.Common.Data
{
    [CreateAssetMenu(menuName = "Data/GameEnvironment", fileName = nameof(GameEnvironmentScriptable))]
    public class GameEnvironmentScriptable : ScriptableObject
    {
        [SerializeField] private string environmentName;
        [SerializeField] private MapConfiguration mapConfiguration;
        [SerializeField] private WorldConfiguration worldConfiguration;
        [SerializeField] private LevelDataScriptable levelData;


#if UNITY_EDITOR
        [TextArea] [SerializeField] [UsedImplicitly]
        private string devInformation;
#endif

        public GameEnvironment ToGameEnvironment()
        {
            return new GameEnvironment(
                mapConfiguration,
                worldConfiguration,
                levelData.ToLevelData(),
                environmentName
            );
        }

        public static GameEnvironmentScriptable Create(string environmentName,
                                                       MapConfiguration mapConfiguration,
                                                       WorldConfiguration worldConfiguration,
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