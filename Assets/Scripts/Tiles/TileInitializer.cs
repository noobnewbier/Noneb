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
    public class TileInitializer : MonoBehaviour
    {
        [SerializeField] private MapConfiguration config;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;


        /// <summary>
        /// Assuming it's a rectangle, reference: https://www.redblobgames.com/grids/hexagons/#map-storage map-storage section
        /// </summary>
        [ContextMenu("InitializeTiles")]
        private void InitializeTiles()
        {
            var tilesTransform = tilesTransformProvider.Provide();

            Assert.AreEqual(tilesTransform.Count, config.ZSize * config.XSize, "Number of tile representation is different from the configuration");

            for (var i = 0; i < config.ZSize; i++)
            for (var j = 0; j < config.XSize; j++)
            {
                var hexGameObject = tilesTransform[i * config.XSize + j].gameObject;
                var tileData = hexGameObject.GetComponent<IObjectProvider<TileData>>().Provide();
                var x = j + i % 2 + i / 2;
                var z = i;

                hexGameObject.GetComponent<TileRepresentation>().Initialize(new Tile(new Coordinate(x, z), null, tileData));
            }
        }
    }
}