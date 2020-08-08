using System.Linq;
using Maps;
using UnityEngine;
using UnityEngine.Serialization;
using WorldConfigurations;

namespace DebugUtils
{
    public class GridOverlay : MonoBehaviour
    {
        [FormerlySerializedAs("config")] [SerializeField] private MapConfiguration mapConfig;
        [SerializeField] private WorldConfigurationProvider worldConfigurationProvider;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;


        private Vector3[] _vertices;

        [ContextMenu("GenerateVertices")]
        private void GenerateVertices()
        {
            var worldConfig = worldConfigurationProvider.Provide();
            var positions = tilesPositionProvider.Provide().ToArray();
            _vertices = new Vector3[mapConfig.XSize * mapConfig.ZSize * 6];

            for (var i = 0; i < mapConfig.ZSize; i++)
            {
                for (var j = 0; j < mapConfig.XSize; j++)
                {
                    for (var k = 0; k < 6; k++)
                    {
                        var vertex = worldConfig.TileCorners[k] + positions[i * mapConfig.XSize + j];

                        _vertices[(i * mapConfig.XSize + j) * 6 + k] = vertex;
                    }
                }
            }
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

            for (var i = 1; i < _vertices.Length; i++)
            {
                Gizmos.DrawLine(_vertices[i], i % 6 == 5 ? _vertices[i - 5] : _vertices[i + 1]);
            }
        }
    }
}