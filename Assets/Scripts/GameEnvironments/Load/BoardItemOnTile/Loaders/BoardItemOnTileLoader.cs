using Common.BoardItems;
using Common.Holders;
using Common.Providers;
using Common.TagInterface;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using Tiles;
using UnityEngine;

namespace GameEnvironments.Load.BoardItemOnTile.Loaders
{
    public abstract class BoardItemOnTileLoader<THolder, TBoardItemOnTile, TData> : MonoBehaviour
        where THolder : Component, IBoardItemHolder<TBoardItemOnTile>
        where TBoardItemOnTile : BoardItem, IOnTile
        where TData : IBoardItemData
    {
        [SerializeField] protected CurrentGameEnvironmentRepositoryProvider gameEnvironmentRepositoryProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        private ILoadBoardItemOnTileService<THolder, TBoardItemOnTile, TData> _loadOnTileService;
        private ICurrentGameEnvironmentRepository _gameEnvironmentRepository;

        private void OnEnable()
        {
            _loadOnTileService = GetService();
            _gameEnvironmentRepository = gameEnvironmentRepositoryProvider.Provide();
        }

        public void Load()
        {
            var gameEnvironment = _gameEnvironmentRepository.Get();
            
            _loadOnTileService.Load(
                GetDatasFromEnvironment(gameEnvironment),
                tilesTransformProvider,
                GetHolderProvider(),
                gameEnvironment.MapConfiguration.XSize,
                gameEnvironment.MapConfiguration.ZSize
            );
        }

        protected abstract ILoadBoardItemOnTileService<THolder, TBoardItemOnTile, TData> GetService();
        protected abstract TData[] GetDatasFromEnvironment(GameEnvironment gameEnvironment);
        protected abstract IGameObjectAndComponentProvider<THolder> GetHolderProvider();
    }
}