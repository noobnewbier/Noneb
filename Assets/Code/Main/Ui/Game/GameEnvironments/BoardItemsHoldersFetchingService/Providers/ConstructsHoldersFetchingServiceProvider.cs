using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Constructs;
using Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers
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