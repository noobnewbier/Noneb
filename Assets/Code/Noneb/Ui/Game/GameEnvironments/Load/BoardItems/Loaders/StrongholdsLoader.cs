using System;
using System.Collections.Immutable;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.CurrentLevelDatas;
using Noneb.Core.Game.Strongholds;
using Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
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