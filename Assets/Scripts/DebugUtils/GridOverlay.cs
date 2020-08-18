using System.Linq;
using Maps.Repositories;
using Maps.Services;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using WorldConfigurations.Repositories;

namespace DebugUtils
{
    public class GridOverlay : MonoBehaviour
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField] private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;
        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField] private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private Transform mapTransform;


        private Vector3[] _vertices;

        [ContextMenu("GenerateVertices")]
        private void GenerateVertices()
        {
            var mapConfigObservable = currentMapConfigRepositoryProvider.Provide().GetObservableStream();
            var worldConfigObservable = currentWorldConfigRepositoryProvider.Provide().GetObservableStream();
            var positionsObservable = tilesPositionServiceProvider.Provide().GetObservableStream(mapTransform.position.y);

            mapConfigObservable.Zip(
                    worldConfigObservable,
                    positionsObservable,
                    (mapConfig, worldConfig, positions) => new {mapConfig, worldConfig, positions}
                )
                .Subscribe(
                    pair =>
                    {
                        var mapConfig = pair.mapConfig;
                        var worldConfig = pair.worldConfig;
                        var positions = pair.positions;

                        _vertices = new Vector3[mapConfig.XSize * mapConfig.ZSize * 6];

                        for (var i = 0; i < mapConfig.ZSize; i++)
                        for (var j = 0; j < mapConfig.XSize; j++)
                        for (var k = 0; k < 6; k++)
                        {
                            var vertex = worldConfig.TileCorners[k] + positions[i * mapConfig.XSize + j];

                            _vertices[(i * mapConfig.XSize + j) * 6 + k] = vertex;
                        }
                    }
                );
        }

        private void OnDrawGizmosSelected()
        {
            if (_vertices == null || !_vertices.Any())
            {
                return;
            }

            var originalColor = Gizmos.color;

            DrawVertices();
            DrawEdges();

            Gizmos.color = originalColor;
        }

        private void DrawVertices()
        {
            Gizmos.color = Color.green;

            foreach (var vertex in _vertices) Gizmos.DrawCube(vertex, Vector3.one * 0.15f);
        }

        private void DrawEdges()
        {
            Gizmos.color = Color.red;

            for (var i = 1; i < _vertices.Length; i++) Gizmos.DrawLine(_vertices[i], i % 6 == 5 ? _vertices[i - 5] : _vertices[i + 1]);
        }
    }
}