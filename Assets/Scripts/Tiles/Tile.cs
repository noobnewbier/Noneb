using System;
using Maps;
using Tiles.Data;
using UnityEngine;

namespace Tiles
{
    [Serializable]
    public class Tile
    {
        //do we actually need coord here?
        [SerializeField] private Coordinate coordinate;
        [SerializeField] private TileData tileData;

        public Tile(Coordinate coordinate,
                    TileData tileData)
        {
            this.coordinate = coordinate;
            this.tileData = tileData;
        }

        public Coordinate Coordinate => coordinate;

        public TileData TileData => tileData;

    }
}