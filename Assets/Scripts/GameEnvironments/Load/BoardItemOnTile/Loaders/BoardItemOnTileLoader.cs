using System.Collections.Immutable;
using Common.BoardItems;
using Common.Holders;
using Common.Providers;
using Common.TagInterface;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
    public abstract class BoardItemOnTileLoader<THolder, TBoardItemOnTile, TData> : MonoBehaviour
        where THolder : Component, IBoardItemHolder<TBoardItemOnTile>
        where TBoardItemOnTile : BoardItem, IOnTile
        where TData : IBoardItemData
    {
        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        [SerializeField] private MapConfigurationProvider mapConfigurationProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        public void Load()
        {
            var mapConfiguration = mapConfigurationProvider.Provide();
            var loadOnTileService = GetService();
            var levelDataRepository = currentLevelDataRepositoryProvider.Provide();
            loadOnTileService.Load(
                GetDatasFromRepository(levelDataRepository),
                tilesTransformProvider,
                GetHolderProvider(),
                mapConfiguration.GetMap2DActualWidth(),
                mapConfiguration.GetMap2DActualHeight()
            );
        }

        protected abstract ILoadBoardItemOnTileService<THolder, TBoardItemOnTile, TData> GetService();
        protected abstract ImmutableArray<TData> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository);
        protected abstract IGameObjectAndComponentProvider<THolder> GetHolderProvider();
    }
}