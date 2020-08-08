using System;
using GameEnvironments.Common.Data.LevelDatas;
using Maps;
using UnityEngine;
using WorldConfigurations;

namespace GameEnvironments.Common.Data
{
    //label everything with int and hence ordering in the palette matters
    //todo: handle overrides
    [Serializable]
    public class GameEnvironmentJson
    {
        [SerializeField] private MapConfigurationJson mapConfigurationJson;
        [SerializeField] private WorldConfigurationJson worldConfigurationJson;
        [SerializeField] private LevelDataJson levelDataJson;

        public GameEnvironmentJson(MapConfigurationJson mapConfigurationJson,
                                   WorldConfigurationJson worldConfigurationJson,
                                   LevelDataJson levelDataJson)
        {
            this.mapConfigurationJson = mapConfigurationJson;
            this.worldConfigurationJson = worldConfigurationJson;
            this.levelDataJson = levelDataJson;
        }

        public MapConfigurationJson MapConfigurationJson => mapConfigurationJson;
        public WorldConfigurationJson WorldConfigurationJson => worldConfigurationJson;
        public LevelDataJson LevelDataJson => levelDataJson;
    }
}