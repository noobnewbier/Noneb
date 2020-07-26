using Constructs;
using Maps;
using Tiles.Holders;
using Units.Holders;
using UnityEngine;
using UnityEngine.Serialization;

namespace DebugUtils
{
    public class MapGenerator : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float chanceOfConstructOnTile;
        [Range(0f, 1f)] [SerializeField] private float chanceOfUnitOnTile;
        [SerializeField] private MapConfiguration config;
        [FormerlySerializedAs("constructRepresentationProvider")] [SerializeField] private ConstructHolderProvider constructHolderProvider;
        [SerializeField] private TileRepresentationProvider tileRepresentationProvider;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [FormerlySerializedAs("unitRepresentationProvider")] [SerializeField] private UnitHoldersProvider unitHoldersProvider;
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