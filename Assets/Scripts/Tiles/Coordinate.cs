using System;
using UnityEngine;

namespace Tiles
{
    //Using Cube Coordinate : https://www.redblobgames.com/grids/hexagons/#map-storage
    [Serializable]
    public struct Coordinate
    {
        [SerializeField] private int x;
        [SerializeField] private int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X => x;

        public int Y => y;

        public int Z => -x - y;
    }
}