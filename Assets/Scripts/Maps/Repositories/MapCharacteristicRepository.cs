
using UnityEngine;

namespace Maps.Repositories
{
    public interface IMapCharacteristicRepository
    {
        int GetMap2DArrayWidth();
        int GetMap2DArrayHeight();
        int GetFlattenMapArrayLength();
        Vector3 GetUpAxis();
        float GetInnerRadius();
        int GetMap2DActualWidth();
        int GetMap2DActualHeight();
    }

    
    //todo: replace all manual calculation of these values
    public class MapCharacteristicRepository : IMapCharacteristicRepository
    {
        private readonly MapConfiguration _mapConfig;

        public MapCharacteristicRepository(MapConfiguration mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public int GetMap2DArrayWidth()
        {
            return _mapConfig.XSize + _mapConfig.ZSize / 2;
        }

        public int GetMap2DArrayHeight()
        {
            return _mapConfig.ZSize;
        }
        
        public int GetMap2DActualWidth()
        {
            return _mapConfig.XSize;
        }

        public int GetMap2DActualHeight()
        {
            return _mapConfig.ZSize;
        }

        public int GetFlattenMapArrayLength()
        {
            return GetMap2DArrayWidth() * GetMap2DArrayHeight();
        }

        public Vector3 GetUpAxis()
        {
            return _mapConfig.UpAxis;
        }

        public float GetInnerRadius()
        {
            return _mapConfig.InnerRadius;
        }
    }
}