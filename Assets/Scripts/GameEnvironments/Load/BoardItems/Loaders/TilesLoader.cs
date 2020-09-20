using System;
using System.Collections.Immutable;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using GameEnvironments.Load.BoardItems.Providers;
using Tiles.Data;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.BoardItems.Loaders
{
    public class TilesLoader : BoardItemsLoader<TileData>
    {
        [SerializeField] private LoadTilesServiceProvider serviceProvider;

        protected override ILoadBoardItemsService<TileData> GetService()
        {
            return serviceProvider.Provide();
        }

        protected override IObservable<ImmutableArray<TileData>> GetDatasFromRepository(ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.TileDatas.ToImmutableArray());
        }
    }
}