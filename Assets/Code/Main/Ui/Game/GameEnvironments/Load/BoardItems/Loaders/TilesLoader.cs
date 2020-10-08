using System;
using System.Collections.Immutable;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.GameEnvironments.Load;
using Main.Core.Game.GameState.CurrentLevelDatas;
using Main.Core.Game.Tiles;
using Main.Ui.Game.GameEnvironments.Load.BoardItems.Providers;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.BoardItems.Loaders
{
    [CreateAssetMenu(fileName = nameof(TilesLoader), menuName = ProjectMenuName.Loader + nameof(TilesLoader))]
    public class TilesLoader : BoardItemsLoader<TileData>
    {
        [SerializeField] private LoadTilesServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<TileData> GetService() => serviceProvider.Provide();

        protected override IObservable<ImmutableArray<TileData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.TileDatas.ToImmutableArray());
        }
    }
}