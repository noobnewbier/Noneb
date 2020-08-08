using GameEnvironments.Common.Repositories.LevelDatas;
using Maps;
using Tiles.Holders;
using UnityEngine;

namespace GameEnvironments.Load.Tiles
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] private MapLoadServiceProvider mapLoadServiceProvider;
        [SerializeField] private MapConfigurationProvider mapConfigurationProvider;
        [SerializeField] private LevelDataRepositoryProvider levelDataRepositoryProvider;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private Transform mapTransform;

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            var mapLoadService = mapLoadServiceProvider.Provide();
            var levelDataRepository = levelDataRepositoryProvider.Provide();
            var mapConfiguration = mapConfigurationProvider.Provide();

            mapLoadService.Load(
                levelDataRepository.TileDatas,
                tilesPositionProvider,
                tileHolderProvider,
                rowPrefab,
                mapTransform,
                mapConfiguration.GetMap2DActualWidth(),
                mapConfiguration.GetMap2DActualHeight()
            );
        }
    }
}