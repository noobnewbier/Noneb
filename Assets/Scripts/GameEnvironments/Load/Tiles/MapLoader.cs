using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using Maps;
using Tiles.Holders;
using UnityEngine;

namespace GameEnvironments.Load.Tiles
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] private MapLoadServiceProvider mapLoadServiceProvider;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private Transform mapTransform;

        private IMapLoadService _mapLoadService;
        private ICurrentGameEnvironmentRepository _currentGameEnvironmentRepository;

        private void OnEnable()
        {
            _mapLoadService = mapLoadServiceProvider.Provide();
            _currentGameEnvironmentRepository = currentGameEnvironmentRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            var gameEnvironment = _currentGameEnvironmentRepository.Get();
            var config = gameEnvironment.MapConfiguration;
            
            _mapLoadService.Load(
                gameEnvironment.TileDatas,
                tilesPositionProvider,
                tileHolderProvider,
                rowPrefab,
                mapTransform,
                config.XSize,
                config.ZSize
            );
        }
    }
}