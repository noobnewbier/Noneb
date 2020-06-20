using System;
using Construct;
using Maps;
using Priority_Queue;
using Tiles;
using Units;
using UnityEngine;

namespace DebugUtils
{
    public class MapGenerator : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float chanceOfConstructOnTile;
        [Range(0f, 1f)] [SerializeField] private float chanceOfUnitOnTile;
        [SerializeField] private MapConfiguration configuration;
        [SerializeField] private ConstructRepresentationProvider constructRepresentationProvider;
        [SerializeField] private TileRepresentationProvider tileRepresentationProvider;
        [SerializeField] private UnitRepresentationProvider unitRepresentationProvider;

        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var selfTransform = transform;
            var upDistance = configuration.OuterRadius * 1.5f;
            var sideDistance = configuration.InnerRadius * 2f;

            for (var i = 0; i < configuration.XSize; i++)
            {
                var sideOffset = i % 2 * sideDistance / 2f;
                for (var j = 0; j < configuration.ZSize; j++)
                {
                    var newTile = tileRepresentationProvider.Provide().gameObject.transform;

                    newTile.parent = selfTransform;

                    switch (configuration.Orientation)
                    {
                        case HexagonOrientation.PointUp:
                            newTile.rotation *= Quaternion.AngleAxis(30f, configuration.UpAxis);
                            newTile.position = new Vector3(
                                j * sideDistance + sideOffset,
                                selfTransform.position.y,
                                i * upDistance
                            );
                            break;
                        case HexagonOrientation.PointSide:
                            newTile.position = new Vector3(
                                i * upDistance,
                                selfTransform.position.y,
                                j * sideDistance + sideOffset
                            );
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}