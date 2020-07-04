using Common;
using Maps;
using Tiles.Data;
using Tiles.Representation;
using UnityEngine;
using UnityEngine.Assertions;

namespace Tiles
{
    /// <summary>
    /// Assuming attached tod parent of all hexagons' rows, which contains the tiles
    /// </summary>
    public class CoordinateSetter : MonoBehaviour
    {
        [SerializeField] private MapConfiguration config;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        /// <summary>
        /// Assuming it's a rectangle, reference: https://www.redblobgames.com/grids/hexagons/#map-storage map-storage section
        /// </summary>
        [ContextMenu("SetCoordinates")]
        private void SetCoordinates()
        {
            var tilesTransform = tilesTransformProvider.Provide();

            Assert.AreEqual(tilesTransform.Count, config.ZSize * config.XSize, "Number of tile representation is different from the configuration");

            for (var i = 0; i < config.ZSize; i++)
            for (var j = 0; j < config.XSize; j++)
            {
                var hexTransform = tilesTransform[i * config.XSize + j];
                var requireCoordinates = hexTransform.GetComponentsInChildren<IRequireCoordinate>();
                var x = j + i % 2 + i / 2;
                var z = i;
                var coordinate = new Coordinate(x, z);
                
                foreach (var requireCoordinate in requireCoordinates)
                {
                    requireCoordinate.Coordinate = coordinate;
                }
            }
        }
    }
}