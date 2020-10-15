using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.BoardItems.Providers;
using Noneb.Ui.Game.Constructs;
using Noneb.Ui.Game.Maps.TilesPosition;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.GameEnvironments.Load.Holders.Providers
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