using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Tiles
{
    [CreateAssetMenu(fileName = nameof(TilesHoldersServiceProvider), menuName = MenuName.ScriptableRepository + nameof(TilesHolderService))]
    public class TilesHoldersServiceProvider : ScriptableObject, IObjectProvider<ITilesHolderService>
    {
        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [SerializeField] private SelectedMapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

        [FormerlySerializedAs("tileHolderssFetchingServiceProvider")] [FormerlySerializedAs("tilesHolderRepositoryProvider")] [SerializeField]
        private TileHoldersFetchingServiceProvider tileHoldersFetchingServiceProvider;


        private ITilesHolderService _cache;

        public ITilesHolderService Provide() =>
            _cache ?? (_cache = new TilesHolderService(
                tileHoldersFetchingServiceProvider.Provide(),
                selectedMapConfigRepositoryProvider.Provide()
            ));
    }
}