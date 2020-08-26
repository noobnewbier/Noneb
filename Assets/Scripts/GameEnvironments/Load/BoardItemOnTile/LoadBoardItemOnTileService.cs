using System.Collections.Generic;
using Common.BoardItems;
using Common.Factories;
using Common.Holders;
using Common.Providers;
using Common.TagInterface;
using Maps;
using Maps.Services;
using UnityEngine;

namespace GameEnvironments.Load.BoardItemOnTile
{
    public interface ILoadBoardItemOnTileService<THolder, in TBoardItem, in TBoardItemData>
        where THolder : Component, IBoardItemHolder<TBoardItem> // THolder is a component and a BoardItemHolder that holds TBoardItem
        where TBoardItem : BoardItem, IOnTile // While TBoardItem, is a BoardItem that is on a tile
        where TBoardItemData : IBoardItemData // and uses IBoardItemData to initialize itself
    {
        void Load(IReadOnlyList<TBoardItemData> boardItemDatas,
                  IList<Transform> tilesTransform,
                  IGameObjectAndComponentProvider<THolder> holderProvider,
                  int mapWidth,
                  int mapHeight);
    }

    public class LoadBoardItemOnTileService<THolder, TBoardItem, TBoardItemData> : ILoadBoardItemOnTileService<THolder, TBoardItem, TBoardItemData>
        where THolder : Component, IBoardItemHolder<TBoardItem>
        where TBoardItem : BoardItem, IOnTile
        where TBoardItemData : IBoardItemData
    {
        private readonly IGetCoordinateService _getCoordinateService;
        private readonly IFactory<TBoardItemData, Coordinate, TBoardItem> _factory;

        public LoadBoardItemOnTileService(IGetCoordinateService getCoordinateService, IFactory<TBoardItemData, Coordinate, TBoardItem> factory)
        {
            _getCoordinateService = getCoordinateService;
            _factory = factory;
        }

        public void Load(IReadOnlyList<TBoardItemData> boardItemDatas,
                         IList<Transform> tilesTransform,
                         IGameObjectAndComponentProvider<THolder> holderProvider,
                         int mapWidth,
                         int mapHeight)
        {

            for (var i = 0; i < mapHeight; i++)
            for (var j = 0; j < mapWidth; j++)
            {
                var index = i * mapWidth + j;
                var boardItemData = boardItemDatas[index];
                if (boardItemData == null)
                {
                    continue;
                }

                var tileTransform = tilesTransform[index];
                var holderAndGo = holderProvider.Provide(tileTransform, false);

                holderAndGo.Component.Initialize(_factory.Create(boardItemData, _getCoordinateService.GetAxialCoordinateFromNestedArrayIndex(j, i)));
            }
        }
    }
}