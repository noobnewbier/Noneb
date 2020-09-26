using System;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps
{
    [CreateAssetMenu(fileName = nameof(MapConfig), menuName = MenuName.Data + nameof(MapConfig))]
    public class MapConfig : ScriptableObject
    {
        private static readonly Lazy<MapConfig> LazyEmpty = new Lazy<MapConfig>(() => Create(0, 0));

        [Range(1, 100)] [SerializeField] private int xSize;
        [Range(1, 100)] [SerializeField] private int zSize;
        public static MapConfig Empty => LazyEmpty.Value;

        public int GetMap2DActualWidth() => xSize;
        public int GetMap2DActualHeight() => zSize;

        // may have to consider making MapConfiguration a plain class and the scriptable a wrapper,
        // don't really like how we have to sort of manually create this
        public static MapConfig Create(int xSize, int zSize)
        {
            var instance = CreateInstance<MapConfig>();

            instance.xSize = xSize;
            instance.zSize = zSize;

            return instance;
        }

        #region Methods - Consider moving them to a service or repo...

        public int GetMap2DArrayWidth()
        {
            return GetMap2DActualWidth() + GetMap2DActualHeight() / 2;
        }

        public int GetMap2DArrayHeight()
        {
            return GetMap2DActualHeight();
        }

        public int GetTotalMapSize()
        {
            return GetMap2DActualWidth() * GetMap2DActualHeight();
        }

        #endregion
    }
}