using Construct;
using Maps;
using Tiles;
using Units;
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
        [SerializeField] private UnitRepresentationProvider unitRepresentationProvider;
        [SerializeField] private GameObject rowPrefab;


        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var selfTransform = transform;
            var upDistance = config.OuterRadius * 1.5f;
            var sideDistance = config.InnerRadius * 2f;

            for (var i = 0; i < config.XSize; i++)
            {
                var row = Instantiate(rowPrefab).transform;
                row.parent = selfTransform;
                var sideOffset = i % 2 * sideDistance / 2f;
                for (var j = 0; j < config.ZSize; j++)
                {
                    var newTile = tileRepresentationProvider.Provide().gameObject.transform;

                    newTile.parent = row;


                    newTile.rotation *= Quaternion.AngleAxis(30f, config.UpAxis);
                    newTile.position = new Vector3(
                        j * sideDistance + sideOffset,
                        selfTransform.position.y,
                        i * upDistance
                    );
                }
            }
        }
    }
}