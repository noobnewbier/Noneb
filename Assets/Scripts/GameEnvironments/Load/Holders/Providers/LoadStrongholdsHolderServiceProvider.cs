using Common.Providers;
using GameEnvironments.Common.Repositories.BoardItems.Providers;
using Maps.Services;
using Strongholds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadStrongholdsHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadStrongholdsHolderService")]
    public class LoadStrongholdsHolderServiceProvider : ScriptableObjectProvider<LoadBoardItemsHolderService<StrongholdHolder, Stronghold>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private StrongholdsRepositoryProvider strongholdsRepositoryProvider;

        [FormerlySerializedAs("strongholdHolderProvider")] [SerializeField]
        private StrongholdHolderFactory strongholdHolderFactory;

        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsHolderService<StrongholdHolder, Stronghold> _cache;

        public override LoadBoardItemsHolderService<StrongholdHolder, Stronghold> Provide() =>
            _cache ?? (_cache = new LoadBoardItemsHolderService<StrongholdHolder, Stronghold>(
                tilesPositionServiceProvider.Provide(),
                strongholdsRepositoryProvider.Provide(),
                strongholdHolderFactory,
                coordinateServiceProvider.Provide()
            ));
    }
}