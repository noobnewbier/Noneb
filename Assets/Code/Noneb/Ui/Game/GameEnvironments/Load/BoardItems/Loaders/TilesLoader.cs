using System;
using System.Collections.Immutable;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.CurrentLevelDatas;
using Noneb.Core.Game.Tiles;
using Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(TilesLoader), menuName = ProjectMenuName.Loader + nameof(TilesLoader))]
    public class TilesLoader : BoardItemsLoader<TileData>
    {
        [SerializeField] private LoadTilesServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<TileData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<TileData>> GetDatasFromRepository(ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.GetMostRecent().Select(d => d.TileDatas.ToImmutableArray());
        }
    }
}