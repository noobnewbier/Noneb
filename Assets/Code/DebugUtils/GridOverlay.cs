using System.Linq;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Core.Game.GameState.WorldConfig;
using Noneb.Ui.Game.Maps.TilesPosition;
using Noneb.Ui.Game.UiState.CurrentMapTransform;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace DebugUtils
{
    public class GridOverlay : MonoBehaviour
    {
        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [SerializeField] private SelectedMapConfigRepositoryProvider selectedMapConfigRepositoryProvider;
        [FormerlySerializedAs("currentWorldConfigRepositoryProvider")] [SerializeField] private SelectedWorldConfigRepositoryProvider selectedWorldConfigRepositoryProvider;
        [SerializeField] private TilesPositionServiceProvider tilesPositionServiceProvider;
        [SerializeField] private CurrentMapTransformRepositoryProvider mapTransformRepositoryProvider;

        private Vector3[] _vertices;

        [ContextMenu("GenerateVertices")]
        private void GenerateVertices()
        {
            var mapConfigObservable = selectedMapConfigRepositoryProvider.Provide().GetObservableStream();
            var worldConfigObservable = selectedWorldConfigRepositoryProvider.Provide().GetObservableStream();
            var mapTransformObservable = mapTransformRepositoryProvider.Provide().GetObservableStream();
            var positionsObservable =
                mapTransformObservable.SelectMany(t => tilesPositionServiceProvider.Provide().GetObservableStream(t.position.y));

            mapConfigObservable.Zip(
                    worldConfigObservable,
                    positionsObservable,
                    (mapConfig, worldConfig, positions) => (mapConfig, worldConfig, positions)
                )
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    tuple =>
                    {
                        var (mapConfig, worldConfig, positions) = tuple;

                        _vertices = new Vector3[mapConfig.GetMap2DActualWidth() * mapConfig.GetMap2DActualHeight() * 6];

                        for (var i = 0; i < mapConfig.GetMap2DActualHeight(); i++)
                        for (var j = 0; j < mapConfig.GetMap2DActualWidth(); j++)
                        for (var k = 0; k < 6; k++)
                        {
                            var vertex = worldConfig.TileCorners[k] + positions[i * mapConfig.GetMap2DActualWidth() + j];

                            _vertices[(i * mapConfig.GetMap2DActualWidth() + j) * 6 + k] = vertex;
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