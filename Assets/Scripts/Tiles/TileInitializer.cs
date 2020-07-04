using Common;
using Maps;
using Tiles.Representation;
using UnityEngine;
using UnityEngine.Assertions;

namespace Tiles
{
    /// <summary>
    /// Dangerously similar to <see cref="CoordinateSetter"/>, consider adding TilesTransformIterator to reduce that WET loop?
    /// </summary>
    public class TileInitializer : MonoBehaviour
    {
        [SerializeField] private MapConfiguration config;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        [ContextMenu("InitializeTiles")]
        private void InitializeTiles()
        {
            var tilesTransform = tilesTransformProvider.Provide();

            Assert.AreEqual(tilesTransform.Count, config.ZSize * config.XSize, "Number of tile representation is different from the configuration");

            for (var i = 0; i < config.ZSize; i++)
            for (var j = 0; j < config.XSize; j++)
            {
                var hexTransform = tilesTransform[i * config.XSize + j];
                var tile = hexTransform.GetComponent<IPreservationContainer<Tile>>().GetPreservation();

                hexTransform.GetComponent<TileRepresentation>().Initialize(tile);
            }
        }
    }
}