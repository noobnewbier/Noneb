using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Repositories.LevelDatas;
using Maps;
using Tiles;
using UnityEngine;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public abstract class GameObjectLoader : MonoBehaviour
    {
        [SerializeField] private GameObjectLoadServiceProvider serviceProvider;
        [SerializeField] private LevelDataRepositoryProvider levelDataRepositoryProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private MapConfigurationProvider mapConfigProvider;


        [ContextMenu(nameof(Load))]
        public void Load()
        {
            var gameObjectLoadService = serviceProvider.Provide();
            var levelDataRepository = levelDataRepositoryProvider.Provide();
            var mapConfig = mapConfigProvider.Provide();

            gameObjectLoadService.Load(
                GetGameObjectProvidersFromRepository(levelDataRepository),
                tilesTransformProvider.Provide(),
                mapConfig.GetMap2DActualWidth(),
                mapConfig.GetMap2DActualHeight()
            );
        }

        protected abstract ImmutableArray<GameObjectProvider> GetGameObjectProvidersFromRepository(ILevelDataRepository levelDataRepository);
    }
}