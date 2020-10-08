using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Strongholds;
using Main.Ui.Game.UiState.BoardItemsFetcherRepository.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
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