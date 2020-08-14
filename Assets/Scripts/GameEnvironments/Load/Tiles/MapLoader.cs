using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Maps.Repositories;
using Tiles.Holders;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.Tiles
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] private MapLoadServiceProvider mapLoadServiceProvider;
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;

        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private Transform mapTransform;

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            var mapLoadService = mapLoadServiceProvider.Provide();
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            var mapConfiguration = mapConfigurationRepositoryProvider.Provide().Get();

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