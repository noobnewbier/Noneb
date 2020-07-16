using System.Linq;
using Maps;
using UnityEngine;

namespace DebugUtils
{
    public class GridOverlay : MonoBehaviour
    {
        [SerializeField] private MapConfiguration config;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;


        private Vector3[] _vertices;

        [ContextMenu("GenerateVertices")]
        private void GenerateVertices()
        {
            var positions = tilesPositionProvider.Provide().ToArray();
            _vertices = new Vector3[config.XSize * config.ZSize * 6];

            for (var i = 0; i < config.ZSize; i++)
            {
                for (var j = 0; j < config.XSize; j++)
                {
                    for (var k = 0; k < 6; k++)
                    {
                        var vertex = config.Corners[k] + positions[i * config.XSize + j];

                        _vertices[(i * config.XSize + j) * 6 + k] = vertex;
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