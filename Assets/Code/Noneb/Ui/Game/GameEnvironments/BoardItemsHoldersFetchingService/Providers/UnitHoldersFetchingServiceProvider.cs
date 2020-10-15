using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers;
using Noneb.Ui.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitHoldersFetchingServiceProvider), menuName = MenuName.ScriptableService + "UnitsHoldersFetchingService")]
    public class UnitHoldersFetchingServiceProvider : ScriptableObject, IObjectProvider<IBoardItemHoldersFetchingService<UnitHolder>>
    {
        [FormerlySerializedAs("providerRepositoryProvider")] [SerializeField]
        private UnitsHolderFetcherRepositoryProvider fetcherRepositoryProvider;

        private IBoardItemHoldersFetchingService<UnitHolder> _cache;

        public IBoardItemHoldersFetchingService<UnitHolder> Provide() =>
            _cache ?? (_cache = new BoardItemHoldersFetchingService<UnitHolder>(
                fetcherRepositoryProvider.Provide()
            ));
    }
}