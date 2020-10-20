using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.GameState.CurrentWorldConfig;
using Noneb.Core.Game.WorldConfigurations;
using Noneb.Core.InGameEditor.Common;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Noneb.Ui.Game.GameEnvironments.Load.Holders;
using Noneb.Ui.Game.Tiles;
using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using Noneb.Ui.InGameEditor.Cameras;
using UniRx;
using UnityEngine;
using UnityUtils;
using Object = UnityEngine.Object;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    public class TileSelectionViewModel : IDisposable
    {
        private readonly IDisposable _compositeDisposable;
        private readonly ICurrentHoveredTileHolderSetRepository _hoveredTileHolderSetRepository;
        private readonly ICurrentSelectedTileHolderSetRepository _currentSelectedTileHolderSetRepository;
        private readonly IDataSetRepository<IInspectable> _currentInspectableSetRepository;
        private readonly Transform _mapTransform;
        private readonly IBoardItemHoldersFetchingService<TileHolder> _holderFetchingService;

        private IReadOnlyList<TileHolder> _currentTileHolders;
        private Camera _currentCamera;
        private WorldConfig _currentWorldConfig;
        private IDisposable _fetchingServiceDisposable;
        private TileHolder _previousClickedTileHolder;
        private bool _haveTilesOnScreen;

        public TileSelectionViewModel(ICurrentWorldConfigRepository worldConfigRepository,
                                      ICurrentHoveredTileHolderSetRepository hoveredTileHolderSetRepository,
                                      ICurrentSelectedTileHolderSetRepository currentSelectedTileHolderSetRepository,
                                      IInGameEditorCameraGetRepository cameraGetRepository,
                                      Transform mapTransform,
                                      IBoardItemHoldersFetchingService<TileHolder> holderFetchingService,
                                      ILoadBoardItemsHolderService tileHolderLoadService,
                                      IDataSetRepository<IInspectable> currentInspectableSetRepository)
        {
            _hoveredTileHolderSetRepository = hoveredTileHolderSetRepository;
            _mapTransform = mapTransform;
            _holderFetchingService = holderFetchingService;
            _currentInspectableSetRepository = currentInspectableSetRepository;
            _currentSelectedTileHolderSetRepository = currentSelectedTileHolderSetRepository;

            _compositeDisposable = new CompositeDisposable
            {
                cameraGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(camera => _currentCamera = camera),
                worldConfigRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(config => _currentWorldConfig = config),
                tileHolderLoadService.FinishedLoadingEventStream
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(_ => UpdateCurrentTileHolders())
            };
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public void OnClicked(Vector3 mousePositionScreenSpace)
        {
            if (!_haveTilesOnScreen)
            {
                return;
            }

            var currentClickedTileHolder = GetClosestTileHolderFromMousePosition(mousePositionScreenSpace);

            if (NotClickedOnSameTile(currentClickedTileHolder))
            {
                UpdateInspectable(currentClickedTileHolder);
                UpdateSelectedTileHolder(currentClickedTileHolder);
            }
        }

        private bool NotClickedOnSameTile(Object currentClickedTileHolder) => _previousClickedTileHolder != currentClickedTileHolder;

        private TileHolder GetClosestTileHolderFromMousePosition(Vector3 mousePositionScreenSpace) =>
            GetClosestTileHolderFromPosition(
                GetMousePositionWorldSpace(mousePositionScreenSpace)
            );

        private void UpdateSelectedTileHolder(TileHolder currentClickedTileHolder)
        {
            _currentSelectedTileHolderSetRepository.Set(
                currentClickedTileHolder
            );
            _previousClickedTileHolder = currentClickedTileHolder;
        }

        private void UpdateInspectable([CanBeNull] TileHolder currentClickedTileHolder)
        {
            _currentInspectableSetRepository.Set(
                currentClickedTileHolder != null ? new InspectableCoordinate(currentClickedTileHolder.Value.Coordinate) : null
            );
        }

        public void OnHover(Vector3 mousePositionScreenSpace)
        {
            if (!_haveTilesOnScreen)
            {
                return;
            }

            _hoveredTileHolderSetRepository.Set(
                GetClosestTileHolderFromMousePosition(mousePositionScreenSpace)
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

        [CanBeNull]
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

        private static bool IsBehaviourValid(Behaviour tileHolder) => tileHolder != null && tileHolder.isActiveAndEnabled;

        private void UpdateCurrentTileHolders()
        {
            _fetchingServiceDisposable = _holderFetchingService.Fetch()
                .Subscribe(
                    holders =>
                    {
                        _haveTilesOnScreen = holders.Count > 0;
                        _currentTileHolders = holders;
                    },
                    () => _fetchingServiceDisposable?.Dispose()
                );
        }
    }
}