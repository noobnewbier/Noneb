using GameEnvironments.Common.Data;
using Tiles;
using UnityEngine;

namespace GameEnvironments.Load.TileGameObject
{
    public class TileGameObjectLoader : MonoBehaviour
    {
        [SerializeField] private TileGameObjectLoadServiceProvider serviceProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private GameEnvironmentScriptable gameEnvironment;

        private ITileGameObjectLoadService _service;

        private void OnEnable()
        {
            _service = serviceProvider.Provide();
        }

        [ContextMenu(nameof(LoadEnvironment))]
        public void LoadEnvironment()
        {
            _service.Load(
                gameEnvironment.TileGameObjectProviders,
                tilesTransformProvider.Provide(),
                gameEnvironment.MapConfiguration.XSize,
                gameEnvironment.MapConfiguration.ZSize
            );
        }
    }
}