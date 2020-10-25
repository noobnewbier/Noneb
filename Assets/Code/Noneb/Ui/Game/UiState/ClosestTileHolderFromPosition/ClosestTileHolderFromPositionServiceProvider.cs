using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.WorldConfig;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using Noneb.Ui.Game.GameEnvironments.Load.Holders.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.ClosestTileHolderFromPosition
{
    [CreateAssetMenu(fileName = nameof(ClosestTileHolderFromPositionServiceProvider), menuName = MenuName.ScriptableService + nameof(ClosestTileHolderFromPositionService))]
    public class ClosestTileHolderFromPositionServiceProvider : ScriptableObject, IObjectProvider<IClosestTileHolderFromPositionService>
    {
        [SerializeField] private WorldConfigRepositoryProvider loadedWorldConfigRepositoryProvider;
        [SerializeField] private TileHoldersFetchingServiceProvider tileHoldersFetchingServiceProvider;
        [SerializeField] private LoadTilesHolderServiceProvider loadTilesHolderServiceProvider;

        private IClosestTileHolderFromPositionService _cache;

        public IClosestTileHolderFromPositionService Provide()
        {
            return _cache ?? (_cache = new ClosestTileHolderFromPositionService(
                loadedWorldConfigRepositoryProvider.Provide(),
                tileHoldersFetchingServiceProvider.Provide(),
                loadTilesHolderServiceProvider.Provide()
            ));
        }
    }
}