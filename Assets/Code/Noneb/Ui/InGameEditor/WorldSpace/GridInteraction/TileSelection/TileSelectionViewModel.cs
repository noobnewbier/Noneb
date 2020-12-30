using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.TagInterface;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.Maps;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.Game.Tiles;
using Noneb.Ui.Game.UiState.ClickStatus;
using Noneb.Ui.Game.UiState.ClosestTileHolderFromPosition;
using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using Noneb.Ui.Game.UiState.CurrentSelectedTileHolder;
using Noneb.Ui.Game.UiState.MousePositionOnMap;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.TileSelection
{
    public class TileSelectionViewModel : IDisposable
    {
        private readonly IDisposable _disposable;
        private readonly ICurrentHoveredTileHolderSetRepository _hoveredTileHolderSetRepository;
        private readonly ICurrentSelectedTileHolderSetRepository _currentSelectedTileHolderSetRepository;
        private readonly IDataSetRepository<IInspectable> _currentInspectableSetRepository;
        private readonly IMousePositionService _mousePositionService;
        private readonly IClosestTileHolderFromPositionService _closestTileHolderFromPositionService;
        private readonly IClickStatusService _clickStatusService;

        private IReadOnlyList<TileHolder> _currentTileHolders;
        private IDisposable _fetchingServiceDisposable;
        private TileHolder _previousClickedTileHolder;
        private bool _haveTilesOnScreen;

        public TileSelectionViewModel(ICurrentHoveredTileHolderSetRepository hoveredTileHolderSetRepository,
                                      ICurrentSelectedTileHolderSetRepository currentSelectedTileHolderSetRepository,
                                      IDataSetRepository<IInspectable> currentInspectableSetRepository,
                                      IClosestTileHolderFromPositionService closestTileHolderFromPositionService,
                                      IMousePositionService mousePositionService,
                                      IMapConfigRepository loadedMapConfigRepository,
                                      IClickStatusService clickStatusService)
        {
            _hoveredTileHolderSetRepository = hoveredTileHolderSetRepository;
            _currentInspectableSetRepository = currentInspectableSetRepository;
            _closestTileHolderFromPositionService = closestTileHolderFromPositionService;
            _mousePositionService = mousePositionService;
            _clickStatusService = clickStatusService;
            _currentSelectedTileHolderSetRepository = currentSelectedTileHolderSetRepository;

            _disposable = loadedMapConfigRepository.GetObservableStream()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(UpdateHaveTilesOnScreen);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private void UpdateHaveTilesOnScreen(MapConfig c)
        {
            _haveTilesOnScreen = IsMapHaveTiles(c);
        }

        private static bool IsMapHaveTiles(MapConfig c) => c.GetTotalMapSize() != 0;

        public void OnClicked(Vector3 mousePositionScreenSpace)
        {
            if (!_haveTilesOnScreen) return;

            var currentClickedTileHolder = GetTileHolderFromMousePosition(mousePositionScreenSpace);

            if (ShouldUpdateSelectedTileHolder(mousePositionScreenSpace, currentClickedTileHolder))
            {
                UpdateInspectable(currentClickedTileHolder);
                UpdateSelectedTileHolder(currentClickedTileHolder);
            }
        }

        private bool ShouldUpdateSelectedTileHolder(Vector3 mousePositionScreenSpace, Object currentClickedTileHolder) => NotClickedOnSameTile(currentClickedTileHolder) && NotClickedOnUi(mousePositionScreenSpace);

        private bool NotClickedOnUi(Vector3 mousePositionScreenSpace) => !_clickStatusService.IsMouseClickOnUi(mousePositionScreenSpace);

        private bool NotClickedOnSameTile(Object currentClickedTileHolder) => _previousClickedTileHolder != currentClickedTileHolder;

        private TileHolder GetTileHolderFromMousePosition(Vector3 mousePositionScreenSpace) =>
            _closestTileHolderFromPositionService.GetTileHolderFromPosition(
                _mousePositionService.GetMousePositionOnMapWorldSpace(mousePositionScreenSpace)
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
            if (!_haveTilesOnScreen) return;

            _hoveredTileHolderSetRepository.Set(
                GetTileHolderFromMousePosition(mousePositionScreenSpace)
            );
        }
    }
}