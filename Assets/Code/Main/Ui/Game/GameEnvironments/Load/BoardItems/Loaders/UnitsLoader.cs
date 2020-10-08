using System;
using System.Collections.Immutable;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.GameState.CurrentLevelDatas;
using Main.Core.Game.Units;
using Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitsLoader), menuName = ProjectMenuName.Loader + nameof(UnitsLoader))]
    public class UnitsLoader : BoardItemsLoader<UnitData>
    {
        [SerializeField] private LoadUnitsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<UnitData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<UnitData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.UnitDatas.ToImmutableArray());
        }
    }
}