using GameEnvironments.Common.Data;
using Maps;
using Tiles.Holders;
using UnityEngine;

namespace GameEnvironments.Load.Map
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] private MapLoadServiceProvider mapLoadServiceProvider;
        [SerializeField] private GameEnvironmentScriptable gameEnvironment;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private Transform mapTransform;

        private IMapLoadService _mapLoadService;

        private void OnEnable()
        {
            _mapLoadService = mapLoadServiceProvider.Provide();
        }

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            var config = gameEnvironment.MapConfiguration;
            _mapLoadService.Load(
                gameEnvironment.TileDatas,
                tilesPositionProvider,
                tileHolderProvider,
                rowPrefab,
                mapTransform,
                config.UpAxis,
                config.XSize,
                config.ZSize
            );
        }
    }
}