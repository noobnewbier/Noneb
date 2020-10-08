using Main.Core.Game.Common.Providers;
using Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers;
using Main.Ui.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
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