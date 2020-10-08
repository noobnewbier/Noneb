using Main.Core.Game.Common.Providers;
using Main.Core.Game.Constructs;
using Main.Core.Game.Coordinate;
using Main.Core.Game.GameEnvironments.BoardItems.Providers;
using Main.Ui.Game.Constructs;
using Main.Ui.Game.Maps.TilesPosition;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.Holders.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadConstructsHolderServiceProvider), menuName = MenuName.ScriptableService + "LoadConstructsHolderService")]
    public class LoadConstructsHolderServiceProvider : ScriptableObject, IObjectProvider<LoadBoardItemsHolderService<ConstructHolder, Construct>>
    {
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;

        [FormerlySerializedAs("constructHolderProvider")] [SerializeField]
        private ConstructHolderFactory constructHolderFactory;

        [SerializeField] private CoordinateServiceProvider coordinateServiceProvider;

        private LoadBoardItemsHolderService<ConstructHolder, Construct> _cache;

        public LoadBoardItemsHolderService<ConstructHolder, Construct> Provide() =>
            _cache ?? (_cache = new LoadBoardItemsHolderService<ConstructHolder, Construct>(
                tilesPositionServiceProvider.Provide(),
                constructsRepositoryProvider.Provide(),
                constructHolderFactory,
                coordinateServiceProvider.Provide()
            ));
    }
}