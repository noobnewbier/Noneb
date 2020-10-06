using System;
using System.Collections.Immutable;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.GameEnvironments.CurrentLevelDatas;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.Strongholds;
using Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdsLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdsLoader))]
    public class StrongholdsLoader : BoardItemsLoader<StrongholdData>
    {
        [SerializeField] private LoadStrongholdsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<StrongholdData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<StrongholdData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdDatas.ToImmutableArray());
        }
    }
}