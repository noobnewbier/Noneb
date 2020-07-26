using System.Collections.Generic;
using System.Linq;
using Maps;
using Tiles.Holders.Repository;
using UnityEngine;

namespace DebugUtils
{
    public class PathShower : MonoBehaviour
    {
        private IList<Vector3> _points;
        [SerializeField] private Coordinate goal;
        [SerializeField] private Coordinate start;
        [SerializeField] private MapProvider mapProvider;
        [Range(0, 65535)] [SerializeField]
        private int maxCost;


        //in axial instead of grid
        [SerializeField] private TileRepresentationRepositoryProvider tileRepresentationRepositoryProvider;


        [ContextMenu("ShowPath")]
        private void ShowPath()
        {
            if (!Pathfinding.TryFindPath(start, goal, mapProvider.Provide(), out var path, maxCost, true))
            {
                Debug.Log("No valid path found");
                _points = null;
                return;
            }

            var tileRepresentationRepository = tileRepresentationRepositoryProvider.Provide();
            var representation = path.Select(c => tileRepresentationRepository.Get(c));
            _points = representation.Select(r => r.transform.position).ToList();
        }

        private void OnDrawGizmosSelected()
        {
            if (_points == null)
            {
                return;
            }

            var originColor = Gizmos.color;

            DrawStartingPoint();
            DrawPath();
            DrawGoalPoint();

            Gizmos.color = originColor;
        }

        private void DrawPath()
        {
            Gizmos.color = Color.red;
            for (var i = 0; i < _points.Count - 1; i++) Gizmos.DrawLine(_points[i], _points[i + 1]);
        }

        private void DrawStartingPoint()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(_points.First(), Vector3.one * 0.25f);
        }

        private void DrawGoalPoint()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(_points.Last(), Vector3.one * 0.25f);
        }
    }
}