using System.Collections.Generic;
using Common.BoardItems;
using Common.Factories;
using GameEnvironments.Common.Repositories.BoardItems;
using Maps;
using Maps.Services;

namespace GameEnvironments.Load.BoardItems
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
        private readonly IGetCoordinateService _getCoordinateService;
        private readonly IFactory<TBoardItemData, Coordinate, TBoardItem> _factory;
        private readonly IBoardItemsSetRepository<TBoardItem> _boardItemsSetRepository;

        public LoadBoardItemsService(IGetCoordinateService getCoordinateService,
                                     IFactory<TBoardItemData, Coordinate, TBoardItem> factory,
                                     IBoardItemsSetRepository<TBoardItem> boardItemsSetRepository)
        {
            _getCoordinateService = getCoordinateService;
            _factory = factory;
            _boardItemsSetRepository = boardItemsSetRepository;
        }

        public void Load(IReadOnlyList<TBoardItemData> boardItemDatas,
                         int mapWidth,
                         int mapHeight)
        {
            var items = new List<TBoardItem>();

            for (var i = 0; i < mapHeight; i++)
            for (var j = 0; j < mapWidth; j++)
            {
                var index = i * mapWidth + j;
                var boardItemData = boardItemDatas[index];
                if (boardItemData == null)
                {
                    continue;
                }

                items.Add(_factory.Create(boardItemData, _getCoordinateService.GetAxialCoordinateFromNestedArrayIndex(j, i)));
            }

            _boardItemsSetRepository.Set(items);
        }
    }
}