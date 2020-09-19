using Common.Factories;
using Common.Providers;
using Constructs;
using Maps;
using Maps.Services;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.Obsolete.BoardItemOnTile.ServiceProviders
{
    [CreateAssetMenu(fileName = nameof(LoadConstructServiceProvider), menuName = MenuName.ScriptableService + "LoadConstructService")]
    public class LoadConstructServiceProvider : ScriptableObjectProvider<ILoadBoardItemOnTileService<ConstructHolder, Construct, ConstructData>>
    {
        [SerializeField] private GetCoordinateServiceProvider coordinateServiceProvider;

        private ILoadBoardItemOnTileService<ConstructHolder, Construct, ConstructData> _onTileService;

        private void OnEnable()
        {
            _onTileService = new LoadBoardItemOnTileService<ConstructHolder, Construct, ConstructData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<ConstructData, Coordinate, Construct>((data, coordinate) => new Construct(data, coordinate))
            );
        }

        public override ILoadBoardItemOnTileService<ConstructHolder, Construct, ConstructData> Provide()
        {
            return _onTileService;
        }
    }
}