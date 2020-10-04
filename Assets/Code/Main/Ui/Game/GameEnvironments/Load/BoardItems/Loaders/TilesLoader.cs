using System;
using System.Collections.Immutable;
using Common;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using GameEnvironments.Load.BoardItems.Providers;
using Tiles.Data;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
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