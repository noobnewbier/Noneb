﻿using UnityEngine;
using UnityUtils.Constants;

namespace Maps
{
    [CreateAssetMenu(fileName = nameof(MapConfiguration), menuName = MenuName.Data + nameof(MapConfiguration))]
    public class MapConfiguration : ScriptableObject
    {
        [Range(1, 10)] [SerializeField] private int xSize;
        [Range(1, 10)] [SerializeField] private int zSize;
        
        //todo: consider removing these two, they are just duplicates atm
        public int XSize => xSize;
        public int ZSize => zSize;

        // may have to consider making MapConfiguration a plain class and the scriptable a wrapper,
        // don't really like how we have to sort of manually create this
        public static MapConfiguration Create(int xSize, int zSize)
        {
            var instance = CreateInstance<MapConfiguration>();

            instance.xSize = xSize;
            instance.zSize = zSize;

            return instance;
        }

        #region Methods - Consider moving them to a service or repo...

        public int GetMap2DArrayWidth()
        {
            return XSize + ZSize / 2;
        }

        public int GetMap2DArrayHeight()
        {
            return ZSize;
        }
        
        public int GetMap2DActualWidth()
        {
            return XSize;
        }

        public int GetMap2DActualHeight()
        {
            return ZSize;
        }

        public int GetFlattenMapArrayLength()
        {
            return GetMap2DArrayWidth() * GetMap2DArrayHeight();
        }

        #endregion
    }
}