using System;
using UnityEngine;

namespace Tiles
{
    //Using Cube Coordinate : https://www.redblobgames.com/grids/hexagons/#map-storage
    [Serializable]
    public struct Coordinate : IEquatable<Coordinate>
    {
        [SerializeField] private int x;
        [SerializeField] private int z;

        public Coordinate(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public int X => x;

        public int Z => z;

        public int Y => -X - Z;

        public bool Equals(Coordinate other)
        {
            return X == other.X && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Z;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X + b.X, a.Z + b.Z);
        }

        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !(a == b);
        }

        public static Coordinate operator -(Coordinate c)
        {
            return new Coordinate(-c.X, -c.Z);
        }

        public static float ManhattanDistance(Coordinate a, Coordinate b)
        {
            return a.X - b.X +
                   (a.Y - b.Y) +
                   (a.Z - b.Z);
        }
    }

    public static class CoordinateExtension
    {
        public static Coordinate AxialToGrid(this Coordinate c)
        {
            return new Coordinate(c.X + c.Z % 2 + c.Z / 2, c.Z);
        }
    }
}