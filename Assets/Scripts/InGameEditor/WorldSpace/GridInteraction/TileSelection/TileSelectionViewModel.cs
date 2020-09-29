using System;
using System.Collections.Generic;
using System.Linq;
using Common.Ui.Repository.CurrentHoveredTileHolder;
using Common.Ui.Repository.CurrentSelectedTileHolder;
using GameEnvironments.Common.Repositories.BoardItemsHolders;
using InGameEditor.Repositories.InGameEditorCamera;
using JetBrains.Annotations;
using Tiles.Holders;
using UniRx;
using UnityEngine;
using UnityUtils;
using WorldConfigurations;
using WorldConfigurations.Repositories;

namespace InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    public class TileSelectionViewModel : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly ICurrentHoveredTileHolderSetRepository _hoveredTileHolderSetRepository;
        private readonly ICurrentSelectedTileHolderSetRepository _currentSelectedTileHolderSetRepository;
        private readonly Transform _mapTransform;

        private IReadOnlyList<TileHolder> _currentTileHolders;
        private Camera _currentCamera;
        private WorldConfig _currentWorldConfig;
        private bool _haveTilesOnScreen;

        public TileSelectionViewModel(ICurrentWorldConfigRepository worldConfigRepository,
                                      ICurrentHoveredTileHolderSetRepository hoveredTileHolderSetRepository,
                                      ICurrentSelectedTileHolderSetRepository currentSelectedTileHolderSetRepository,
                                      IInGameEditorCameraGetRepository cameraGetRepository,
                                      Transform mapTransform,
                                      IBoardItemsHolderGetRepository<TileHolder> holderGetRepository)
        {
            _hoveredTileHolderSetRepository = hoveredTileHolderSetRepository;
            _mapTransform = mapTransform;
            _currentSelectedTileHolderSetRepository = currentSelectedTileHolderSetRepository;

            _disposable = new CompositeDisposable
            {
                cameraGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(camera => _currentCamera = camera),
                holderGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(
                        holders =>
                        {
                            _haveTilesOnScreen = holders != null && holders.Count > 0;
                            _currentTileHolders = holders;
                        }
                    ),
                worldConfigRepository.GetObservableStream().Subscribe(config => _currentWorldConfig = config)
            };
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void OnClicked(Vector3 mousePositionScreenSpace)
        {
            if (!_haveTilesOnScreen)
            {
                return;
            }

            _currentSelectedTileHolderSetRepository.Set(
                GetClosestTileHolderFromPosition(
                    GetMousePositionWorldSpace(mousePositionScreenSpace)
                )
            );
        }

        public void OnHover(Vector3 mousePositionScreenSpace)
        {
            if (!_haveTilesOnScreen)
            {
                return;
            }

            _hoveredTileHolderSetRepository.Set(
                GetClosestTileHolderFromPosition(
                    GetMousePositionWorldSpace(mousePositionScreenSpace)
                )
            );
        }

        private Vector3 GetMousePositionWorldSpace(Vector3 mousePositionScreenSpace)
        {
            //any distance greater than 0 should work, we only need to form a line equation anyway.
            mousePositionScreenSpace.z = 1f;
            var startingPoint = _currentCamera.transform.position;
            var endPoint = _currentCamera.ScreenToWorldPoint(mousePositionScreenSpace);

            var mapY = _mapTransform.position.y;
            var (x, z) = LineEquations.GetXzGivenY(mapY, startingPoint, endPoint);


            return new Vector3(x, mapY, z);
        }

        private TileHolder GetClosestTileHolderFromPosition(Vector3 position)
        {
            // we don't want to highlight tiles that is literally not even touched by our cursor
            var minDistanceThreshold = _currentWorldConfig.OuterRadius;

            //calculation is done twice on a few tiles, doesn't really matter
            var holdersWithinThreshold = _currentTileHolders
                .Where(IsBehaviourValid)
                .Where(h => Vector3.Distance(h.transform.position, position) < minDistanceThreshold)
                .ToArray();

            return holdersWithinThreshold.Any() ?
                holdersWithinThreshold.MinBy(h => Vector3.Distance(h.transform.position, position)) :
                null;
        }

        private bool IsBehaviourValid(Behaviour tileHolder)
        {
            return tileHolder != null && tileHolder.isActiveAndEnabled;
        }
    }
}