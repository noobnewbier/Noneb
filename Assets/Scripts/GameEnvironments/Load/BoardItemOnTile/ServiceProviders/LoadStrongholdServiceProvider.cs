using Common.Factories;
using Common.Providers;
using Maps;
using Maps.Services;
using Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.BoardItemOnTile.ServiceProviders
{
    [CreateAssetMenu(fileName = nameof(LoadStrongholdServiceProvider), menuName = MenuName.ScriptableService + "LoadStrongholdService")]
    public class LoadStrongholdServiceProvider: ScriptableObjectProvider<ILoadBoardItemOnTileService<StrongholdHolder, Stronghold, StrongholdData>>
    {
        [SerializeField] private GetCoordinateServiceProvider coordinateServiceProvider;

        private ILoadBoardItemOnTileService<StrongholdHolder, Stronghold, StrongholdData> _onTileService;

        private void OnEnable()
        {
            _onTileService = new LoadBoardItemOnTileService<StrongholdHolder, Stronghold, StrongholdData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<StrongholdData, Coordinate, Stronghold>((data, coordinate) => new Stronghold(data, coordinate))
            );
        }

        public override ILoadBoardItemOnTileService<StrongholdHolder, Stronghold, StrongholdData> Provide()
        {
            return _onTileService;
        }
    }
}