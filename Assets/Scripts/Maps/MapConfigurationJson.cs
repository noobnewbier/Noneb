using System;
using UnityEngine;

namespace Maps
{
    [Serializable]
    public class MapConfigurationJson
    {
        [SerializeField] private int xSize;
        [SerializeField] private int zSize;

        public MapConfigurationJson(int xSize, int zSize)
        {
            this.xSize = xSize;
            this.zSize = zSize;
        }

        public int XSize => xSize;

        public int ZSize => zSize;
    }
}