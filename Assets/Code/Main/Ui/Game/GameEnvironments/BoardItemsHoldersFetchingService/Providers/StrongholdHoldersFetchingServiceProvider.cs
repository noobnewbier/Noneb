using Main.Core.Game.Common.Providers;
using Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers;
using Main.Ui.Game.Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdHoldersFetchingServiceProvider),
        menuName = MenuName.ScriptableService + "StrongholdsHoldersFetchingService"
    )]
    public class StrongholdHoldersFetchingServiceProvider : ScriptableObjectProvider<BoardItemHoldersFetchingService<StrongholdHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private StrongholdsHolderFetcherRepositoryProvider fetcherRepositoryProvider;


        private BoardItemHoldersFetchingService<StrongholdHolder> _cache;

        public override BoardItemHoldersFetchingService<StrongholdHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<StrongholdHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}