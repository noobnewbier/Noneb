using Constructs;
using Maps;
using Tiles.Representation;
using Units.Representation;
using UnityEngine;

namespace DebugUtils
{
    public class MapGenerator : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float chanceOfConstructOnTile;
        [Range(0f, 1f)] [SerializeField] private float chanceOfUnitOnTile;
        [SerializeField] private MapConfiguration config;
        [SerializeField] private ConstructRepresentationProvider constructRepresentationProvider;
        [SerializeField] private TileRepresentationProvider tileRepresentationProvider;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [SerializeField] private UnitRepresentationProvider unitRepresentationProvider;
        [SerializeField] private GameObject rowPrefab;


        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var selfTransform = transform;
            var positions = tilesPositionProvider.Provide();

            for (var i = 0; i < config.ZSize; i++)
            {
                var row = Instantiate(rowPrefab).transform;
                row.parent = selfTransform;
                for (var j = 0; j < config.XSize; j++)
                {
                    var newTile = tileRepresentationProvider.Provide().gameObject.transform;

                    newTile.parent = row;
                    newTile.rotation *= Quaternion.AngleAxis(30f, config.UpAxis);
                    newTile.position = positions[i * config.XSize + j];
                }
            }
        }
    }
}