using System;
using JetBrains.Annotations;
using Maps;
using Tiles.Data;
using Units;
using UnityEngine;

namespace Tiles
{
    [Serializable]
    public class Tile
    {
        //do we actually need coord here?
        [SerializeField] private Coordinate coordinate;
        [SerializeField] [CanBeNull] private Unit occupier;
        [SerializeField] private TileData tileData;

        public Tile(Coordinate coordinate,
                    [CanBeNull] Unit occupier,
                    TileData tileData)
        {
            this.coordinate = coordinate;
            this.occupier = occupier;
            this.tileData = tileData;
        }

        public Coordinate Coordinate => coordinate;

        public TileData TileData => tileData;

        [CanBeNull] public Unit Occupier => occupier;
    }
}