using System;
using System.Collections.Generic;
using Noneb.Ui.Game.Cameras;
using Noneb.Ui.Game.Tiles;
using Noneb.Ui.Game.UiState.CurrentMapTransform;
using UniRx;
using UnityEngine;
using UnityUtils;

namespace Noneb.Ui.Game.UiState.MousePositionOnMap
{
    public interface IMousePositionService : IDisposable
    {
        Vector3 GetMousePositionOnMapWorldSpace(Vector3 mousePositionScreenSpace);
    }

    public class MousePositionOnMapService : IMousePositionService
    {
        private readonly IDisposable _disposable;

        private IReadOnlyList<TileHolder> _currentTileHolders;
        private Transform _mapTransform;
        private Camera _currentCamera;
        private IDisposable _fetchingServiceDisposable;
        private TileHolder _previousClickedTileHolder;
        private bool _haveTilesOnScreen;

        public MousePositionOnMapService(ICameraGetRepository cameraGetRepository, ICurrentMapTransformGetRepository mapTransformGetRepository)
        {
            _disposable = new CompositeDisposable
            {
                mapTransformGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(t => _mapTransform = t),

                cameraGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(camera => _currentCamera = camera)
            };
        }


        public Vector3 GetMousePositionOnMapWorldSpace(Vector3 mousePositionScreenSpace)
        {
            //any distance greater than 0 should work, we only need to form a line equation.
            mousePositionScreenSpace.z = 1f;
            var startingPoint = _currentCamera.transform.position;
            var endPoint = _currentCamera.ScreenToWorldPoint(mousePositionScreenSpace);

            var mapY = _mapTransform.position.y;
            var (x, z) = LineEquations.GetXzGivenY(mapY, startingPoint, endPoint);


            return new Vector3(x, mapY, z);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}