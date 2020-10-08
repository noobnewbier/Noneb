using System;
using System.Collections.Generic;
using System.Linq;
using Main.Core.Game.Coordinate;
using Main.Core.Game.GameState.Map;
using Main.Core.Game.Maps;
using Main.Ui.Game.Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace DebugUtils
{
    public class PathShower : MonoBehaviour
    {
        private IList<Vector3> _points;
        private IDisposable _disposable;
        [SerializeField] private Coordinate goal;
        [SerializeField] private Coordinate start;

        [Range(0, 65535)] [SerializeField] private int maxCost;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;

        //in axial instead of grid
        [FormerlySerializedAs("tileHoldersRepositoryProvider")]
        [FormerlySerializedAs("tileHolderRepositoryProvider")]
        [FormerlySerializedAs("tileRepresentationRepositoryProvider")]
        [SerializeField]
        private TilesHoldersServiceProvider tilesHoldersServiceProvider;


        [ContextMenu(nameof(ShowPath))]
        private void ShowPath()
        {
            var mapRepository = mapRepositoryProvider.Provide();

            _disposable?.Dispose();
            _disposable = mapRepository.GetMostRecent()
                .Select(
                    map =>
                    {
                        if (!Pathfinding.TryFindPath(start, goal, map, out var path, maxCost, true))
                        {
                            _points = null;
                            throw new InvalidOperationException("No valid path found");
                        }

                        return path;
                    }
                )
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .SelectMany(
                    path =>
                    {
                        var tileHoldersRepository = tilesHoldersServiceProvider.Provide();
                        return path.Select(c => tileHoldersRepository.GetAtCoordinateSingle(c)).Zip();
                    }
                )
                .Subscribe(
                    holders => { _points = holders.Select(r => r.transform.position).ToList(); },
                    e =>
                    {
                        if (e is InvalidOperationException)
                        {
                            Debug.Log("No valid path found");
                        }
                        else
                        {
                            throw e;
                        }
                    }
                );
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
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