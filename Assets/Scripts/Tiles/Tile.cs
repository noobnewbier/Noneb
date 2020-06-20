using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Units;
using UnityEngine;

// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Tiles
{
    [Serializable]
    public struct Tile : IEquatable<Tile>
    {
        //do we actually need coord here?
        [SerializeField] private Coordinate coordinate;
        [SerializeField] [CanBeNull] private Unit occupier;
        [SerializeField] private TileData tileData;
        [SerializeField] private IReadOnlyDictionary<HexDirection, Tile> neighbours;

        public Tile(Coordinate coordinate, [CanBeNull] Unit occupier, TileData tileData,
            IReadOnlyDictionary<HexDirection, Tile> neighbours)
        {
            this.coordinate = coordinate;
            this.occupier = occupier;
            this.tileData = tileData;
            this.neighbours = neighbours;
        }

        public IReadOnlyDictionary<HexDirection, Tile> Neighbours => neighbours;

        public Coordinate Coordinate => coordinate;

        public TileData TileData => tileData;

        [CanBeNull] public Unit Occupier => occupier;

        public bool Equals(Tile other)
        {
            return coordinate.Equals(other.coordinate);
        }

        public override bool Equals(object obj)
        {
            return obj is Tile other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = coordinate.GetHashCode();
                hashCode = (hashCode * 397) ^ (occupier != null ? occupier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (tileData != null ? tileData.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (neighbours != null ? neighbours.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Tile t1, Tile t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(Tile t1, Tile t2)
        {
            return !(t1 == t2);
        }
    }
}