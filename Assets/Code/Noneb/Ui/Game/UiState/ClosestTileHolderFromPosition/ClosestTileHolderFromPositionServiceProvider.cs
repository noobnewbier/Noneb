using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.WorldConfig;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using Noneb.Ui.Game.GameEnvironments.Load.Holders.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.UiState
{
    [CreateAssetMenu(fileName = nameof(ClosestTileHolderFromPositionServiceProvider), menuName = MenuName.ScriptableService + nameof(ClosestTileHolderFromPositionService))]
    public class ClosestTileHolderFromPositionServiceProvider : ScriptableObject, IObjectProvider<IClosestTileHolderFromPositionService>
    {
        [FormerlySerializedAs("currentWorldConfigRepositoryProvider")] [SerializeField] private SelectedWorldConfigRepositoryProvider selectedWorldConfigRepositoryProvider;
        [SerializeField] private TileHoldersFetchingServiceProvider tileHoldersFetchingServiceProvider;
        [SerializeField] private LoadTilesHolderServiceProvider loadTilesHolderServiceProvider;

        private IClosestTileHolderFromPositionService _cache;

        public IClosestTileHolderFromPositionService Provide()
        {
            return _cache ?? (_cache = new ClosestTileHolderFromPositionService(
                selectedWorldConfigRepositoryProvider.Provide(),
                tileHoldersFetchingServiceProvider.Provide(),
                loadTilesHolderServiceProvider.Provide()
            ));
        }
    }
}