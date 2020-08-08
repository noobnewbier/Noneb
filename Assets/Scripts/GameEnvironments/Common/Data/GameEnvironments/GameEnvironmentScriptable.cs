using System;
using System.Linq;
using Common.Providers;
using Constructs;
using GameEnvironments.Common.Data.LevelDatas;
using JetBrains.Annotations;
using Maps;
using Strongholds;
using Tiles.Data;
using Units;
using UnityEngine;
using WorldConfigurations;

namespace GameEnvironments.Common.Data.GameEnvironments
{
    [CreateAssetMenu(menuName = "Data/GameEnvironment", fileName = nameof(GameEnvironmentScriptable))]
    public class GameEnvironmentScriptable : ScriptableObject
    {
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
                levelData.ToLevelData()
            );
        }

        public static GameEnvironmentScriptable Create(MapConfiguration mapConfiguration, WorldConfiguration worldConfiguration, LevelDataScriptable levelDataScriptable)
        {
            var newInstance = CreateInstance<GameEnvironmentScriptable>();
            
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