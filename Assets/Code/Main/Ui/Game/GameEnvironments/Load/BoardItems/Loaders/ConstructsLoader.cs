using System;
using System.Collections.Immutable;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.Constructs;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.GameState.CurrentLevelDatas;
using Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructsLoader), menuName = ProjectMenuName.Loader + nameof(ConstructsLoader))]
    public class ConstructsLoader : BoardItemsLoader<ConstructData>
    {
        [SerializeField] private LoadConstructsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<ConstructData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<ConstructData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.ConstructDatas.ToImmutableArray());
        }
    }
}