﻿using System;
using System.Collections.Generic;
using System.Linq;
using Maps;
using Maps.Repositories;
using Tiles.Holders.Repository;
using UnityEngine;
using UnityEngine.Serialization;
using UniRx;

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
        [FormerlySerializedAs("tileRepresentationRepositoryProvider")] [SerializeField]
        private TileHolderRepositoryProvider tileHolderRepositoryProvider;


        [ContextMenu(nameof(ShowPath))]
        private void ShowPath()
        {
            var mapRepository = mapRepositoryProvider.Provide();

            _disposable = mapRepository.Get()
                .Subscribe(
                    map =>
                    {
                        if (!Pathfinding.TryFindPath(start, goal, map, out var path, maxCost, true))
                        {
                            Debug.Log("No valid path found");
                            _points = null;
                            return;
                        }

                        var tileHoldersRepository = tileHolderRepositoryProvider.Provide();
                        var holders = path.Select(c => tileHoldersRepository.Get(c));
                        _points = holders.Select(r => r.transform.position).ToList();
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