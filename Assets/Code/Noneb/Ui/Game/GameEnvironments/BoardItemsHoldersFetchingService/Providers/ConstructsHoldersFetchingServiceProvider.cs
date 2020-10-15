using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.Constructs;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
{
    [CreateAssetMenu(
        fileName = nameof(ConstructsHoldersFetchingServiceProvider),
        menuName = MenuName.ScriptableService + "ConstructsHoldersFetchingService"
    )]
    public class ConstructsHoldersFetchingServiceProvider : ScriptableObject, IObjectProvider<IBoardItemHoldersFetchingService<ConstructHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private ConstructsHolderFetcherRepositoryProvider fetcherRepositoryProvider;

        private BoardItemHoldersFetchingService<ConstructHolder> _cache;

        public IBoardItemHoldersFetchingService<ConstructHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<ConstructHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}