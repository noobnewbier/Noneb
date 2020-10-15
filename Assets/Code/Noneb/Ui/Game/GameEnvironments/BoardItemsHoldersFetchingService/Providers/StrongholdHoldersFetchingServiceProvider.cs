using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Strongholds;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdHoldersFetchingServiceProvider),
        menuName = MenuName.ScriptableService + "StrongholdsHoldersFetchingService"
    )]
    public class StrongholdHoldersFetchingServiceProvider : ScriptableObject, IObjectProvider<IBoardItemHoldersFetchingService<StrongholdHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private StrongholdsHolderFetcherRepositoryProvider fetcherRepositoryProvider;


        private BoardItemHoldersFetchingService<StrongholdHolder> _cache;

        public IBoardItemHoldersFetchingService<StrongholdHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<StrongholdHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}