using System;

namespace Tiles
{
    //Using Cube Coordinate : https://www.redblobgames.com/grids/hexagons/#map-storage
    [Serializable]
    public struct Coordinate : IEquatable<Coordinate>
    {
        public Coordinate(int x, int z)
        {
            X = x;
            Z = z;
        }

        public int X { get; }

        public int Z { get; }

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
    }
}