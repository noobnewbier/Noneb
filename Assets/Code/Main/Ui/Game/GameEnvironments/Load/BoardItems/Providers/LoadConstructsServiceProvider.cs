using Main.Core.Game.Common.Factories;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Constructs;
using Main.Core.Game.Coordinates;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.GameState.BoardItems.Providers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(LoadConstructsServiceProvider), menuName = MenuName.ScriptableService + "LoadConstructsService")]
    public class LoadConstructsServiceProvider : ScriptableObject, IObjectProvider<ILoadBoardItemsService<ConstructData>>
    {
        [SerializeField] private ConstructsRepositoryProvider constructsRepositoryProvider;

        [FormerlySerializedAs("getCoordinateServiceProvider")] [SerializeField]
        private CoordinateServiceProvider coordinateServiceProvider;

        private ILoadBoardItemsService<ConstructData> _cache;

        public ILoadBoardItemsService<ConstructData> Provide()
        {
            return _cache ?? (_cache = new LoadBoardItemsService<Construct, ConstructData>(
                coordinateServiceProvider.Provide(),
                Factory.Create<ConstructData, Coordinate, Construct>
                    ((data, coordinate) => new Construct(data, coordinate)),
                constructsRepositoryProvider.Provide()
            ));
        }
    }
}