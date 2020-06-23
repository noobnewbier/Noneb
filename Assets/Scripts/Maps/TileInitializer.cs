using System.Collections.Generic;
using Common;
using Tiles;
using UnityEngine;
using UnityEngine.Assertions;

namespace Maps
{
    /// <summary>
    /// Assuming attached to parent of all hexagons' rows, which contains the tiles
    /// </summary>
    public class TileInitializer : MonoBehaviour
    {
        [SerializeField] private MapConfiguration config;

        private IList<Transform> GetRows()
        {
            var toReturn = new List<Transform>();
            for (var i = 0; i < transform.childCount; i++)
            {
                //do our best to ensure it is in order
                var child = transform.GetChild(i);
                if (child.CompareTag(ObjectTags.GridRow))
                {
                    toReturn.Add(child);
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Assuming it's a rectangle, reference: https://www.redblobgames.com/grids/hexagons/#map-storage map-storage section
        /// </summary>
        [ContextMenu("InitializeTiles")]
        private void InitializeTiles()
        {
            var rows = GetRows();

            Assert.AreEqual(rows.Count, config.ZSize, "number of rows in this game object is different from the configuration");

            for (var i = 0; i < rows.Count; i++)
            for (var j = 0; j < config.XSize; j++)
            {
                var hexGameObject = rows[i].GetChild(j);
                var tileData = hexGameObject.GetComponent<IObjectProvider<TileData>>().Provide();
                var x = j + i % 2 + i / 2;
                var z = -i;

                hexGameObject.GetComponent<TileRepresentation>().Initialize(new Tile(new Coordinate(x, z), null, tileData));
            }
        }
    }
}