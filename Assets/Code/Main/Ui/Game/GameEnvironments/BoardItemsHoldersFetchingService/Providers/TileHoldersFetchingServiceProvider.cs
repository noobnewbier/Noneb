using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Tiles;
using Main.Ui.Game.UiState.BoardItemsFetcherRepository.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
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