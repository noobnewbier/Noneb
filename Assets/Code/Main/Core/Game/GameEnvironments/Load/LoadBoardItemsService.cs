using System.Collections.Generic;
using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Coordinate;
using Main.Core.Game.GameEnvironments.BoardItems;

namespace Main.Core.Game.GameEnvironments.Load
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
        private readonly IFactory<TBoardItemData, Coordinate.Coordinate, TBoardItem> _factory;
        private readonly IBoardItemsSetRepository<TBoardItem> _boardItemsSetRepository;

        public LoadBoardItemsService(ICoordinateService coordinateService,
                                     IFactory<TBoardItemData, Coordinate.Coordinate, TBoardItem> factory,
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

                items.Add(_factory.Create(boardItemData, _coordinateService.GetAxialCoordinateFromNestedArrayIndex(j, i)));
            }

            _boardItemsSetRepository.Set(items);
        }
    }
}