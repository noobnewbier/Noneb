using System;
using System.Collections.Generic;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.BoardItems;

namespace Noneb.Core.Game.GameEnvironments.Load
{
    public interface ILoadBoardItemsService<in TBoardItemData> where TBoardItemData : BoardItemData
    {
        void Load(IReadOnlyList<TBoardItemData> boardItemDatas,
                  int mapWidth,
                  int mapHeight);
    }

    public class LoadBoardItemsService<TBoardItem, TBoardItemData> : ILoadBoardItemsService<TBoardItemData>
        where TBoardItem : BoardItem
        where TBoardItemData : BoardItemData
    {
        private readonly ICoordinateService _coordinateService;
        private readonly IFactory<TBoardItemData, Coordinate, TBoardItem> _factory;
        private readonly IBoardItemsSetRepository<TBoardItem> _boardItemsSetRepository;

        public LoadBoardItemsService(ICoordinateService coordinateService,
                                     IFactory<TBoardItemData, Coordinate, TBoardItem> factory,
                                     IBoardItemsSetRepository<TBoardItem> boardItemsSetRepository)
        {
            _coordinateService = coordinateService;
            _factory = factory;
            _boardItemsSetRepository = boardItemsSetRepository;
        }

        public void Load(IReadOnlyList<TBoardItemData> boardItemDatas,
                         int mapWidth,
                         int mapHeight)
        {
            if (boardItemDatas.Count > mapWidth * mapHeight)
                throw new ArgumentException($"Supplied amount of datas({boardItemDatas.Count}) is larger than map's size ({mapWidth * mapHeight})");

            var items = new List<TBoardItem>();

            for (var i = 0; i < mapHeight; i++)
            for (var j = 0; j < mapWidth; j++)
            {
                var index = i * mapWidth + j;
                var boardItemData = boardItemDatas[index];
                if (boardItemData == null) continue;

                items.Add(_factory.Create(boardItemData, _coordinateService.GetAxialCoordinateFromNestedArrayIndex(j, i)));
            }

            _boardItemsSetRepository.Set(items);
        }
    }
}