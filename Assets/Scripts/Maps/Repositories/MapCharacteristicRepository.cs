
using UnityEngine;

namespace Maps.Repositories
{
    public interface IMapCharacteristicRepository
    {
        int GetMap2DArrayWidth();
        int GetMap2dArrayHeight();
        int GetFlattenMapArrayWidth();
        Vector3 GetUpAxis();
        float GetInnerRadius();
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

        public int GetMap2dArrayHeight()
        {
            return _mapConfig.ZSize;
        }

        public int GetFlattenMapArrayWidth()
        {
            return GetMap2DArrayWidth() * GetMap2dArrayHeight();
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