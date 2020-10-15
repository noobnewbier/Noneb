using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Tiles;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
{
    [CreateAssetMenu(fileName = nameof(TileHoldersFetchingServiceProvider), menuName = MenuName.ScriptableService + "TileHoldersFetchingService")]
    public class TileHoldersFetchingServiceProvider : ScriptableObject, IObjectProvider<IBoardItemHoldersFetchingService<TileHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private TilesHolderFetcherRepositoryProvider fetcherRepositoryProvider;

        private IBoardItemHoldersFetchingService<TileHolder> _cache;

        public IBoardItemHoldersFetchingService<TileHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<TileHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}