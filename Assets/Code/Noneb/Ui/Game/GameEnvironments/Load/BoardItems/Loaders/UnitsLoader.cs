using System;
using System.Collections.Immutable;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.CurrentLevelDatas;
using Noneb.Core.Game.Units;
using Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitsLoader), menuName = ProjectMenuName.Loader + nameof(UnitsLoader))]
    public class UnitsLoader : BoardItemsLoader<UnitData>
    {
        [SerializeField] private LoadUnitsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<UnitData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<UnitData>> GetDatasFromRepository(ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.GetMostRecent().Select(d => d.UnitDatas.ToImmutableArray());
        }
    }
}