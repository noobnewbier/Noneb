using System;
using UnityEngine;

namespace ObsoleteJsonRelated
{
    //label everything with int and hence ordering in the palette matters
    //todo: handle overrides
    [Serializable]
    public class GameEnvironmentJson
    {
        [SerializeField] private string environmentName;
        [SerializeField] private MapConfigurationJson mapConfigurationJson;
        [SerializeField] private WorldConfigurationJson worldConfigurationJson;
        [SerializeField] private LevelDataJson levelDataJson;

        public GameEnvironmentJson(MapConfigurationJson mapConfigurationJson,
                                   WorldConfigurationJson worldConfigurationJson,
                                   LevelDataJson levelDataJson,
                                   string environmentName)
        {
            this.environmentName = environmentName;
            this.mapConfigurationJson = mapConfigurationJson;
            this.worldConfigurationJson = worldConfigurationJson;
            this.levelDataJson = levelDataJson;
        }

        public string EnvironmentName => environmentName;

        public MapConfigurationJson MapConfigurationJson => mapConfigurationJson;
        public WorldConfigurationJson WorldConfigurationJson => worldConfigurationJson;
        public LevelDataJson LevelDataJson => levelDataJson;
    }
}