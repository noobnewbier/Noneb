using System;
using System.Collections.Immutable;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.CurrentLevelDatas;
using Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructsLoader), menuName = ProjectMenuName.Loader + nameof(ConstructsLoader))]
    public class ConstructsLoader : BoardItemsLoader<ConstructData>
    {
        [SerializeField] private LoadConstructsServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<ConstructData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<ConstructData>> GetDatasFromRepository(ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.GetMostRecent().Select(d => d.ConstructDatas.ToImmutableArray());
        }
    }
}