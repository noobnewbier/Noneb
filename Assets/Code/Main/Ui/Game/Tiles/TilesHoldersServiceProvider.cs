using Main.Core.Game.Common.Providers;
using Main.Core.Game.Maps.CurrentMapConfig;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.Tiles
{
    [CreateAssetMenu(fileName = nameof(TilesHoldersServiceProvider), menuName = MenuName.ScriptableRepository + nameof(TilesHolderService))]
    public class TilesHoldersServiceProvider : ScriptableObjectProvider<ITilesHolderService>
    {
        [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("tileHolderssFetchingServiceProvider")] [FormerlySerializedAs("tilesHolderRepositoryProvider")] [SerializeField]
        private TileHoldersFetchingServiceProvider tileHoldersFetchingServiceProvider;


        private ITilesHolderService _cache;

        public override ITilesHolderService Provide() =>
            _cache ?? (_cache = new TilesHolderService(
                tileHoldersFetchingServiceProvider.Provide(),
                currentMapConfigRepositoryProvider.Provide()
            ));
    }
}