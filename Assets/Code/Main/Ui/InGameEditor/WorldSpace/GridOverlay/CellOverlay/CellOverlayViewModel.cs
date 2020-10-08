using Experiment.CrossPlatformLiveData;
using Main.Core.Game.Coordinate;
using Main.Core.Game.WorldConfigurations;
using UniRx;
using UnityEngine;

namespace Main.Ui.InGameEditor.WorldSpace.GridOverlay.CellOverlay
{
    public class CellOverlayViewModel
    {
        public CellOverlayViewModel(bool coordinateVisibility, bool cellVisibility)
        {
            PositionLiveData = new LiveData<Vector3>();
            WorldConfigLiveData = new LiveData<WorldConfig>();
            CoordinateVisibilityLiveData = new LiveData<bool>(coordinateVisibility);
            LineVisibilityLiveData = new LiveData<bool>(cellVisibility);
            CoordinateLiveData = new LiveData<Coordinate>();
            DestructionInstructionLiveData = new LiveData<Unit>();
        }

        public ILiveData<Vector3> PositionLiveData { get; }
        public ILiveData<WorldConfig> WorldConfigLiveData { get; }
        public ILiveData<bool> CoordinateVisibilityLiveData { get; }
        public ILiveData<bool> LineVisibilityLiveData { get; }
        public ILiveData<Coordinate> CoordinateLiveData { get; }
        public ILiveData<Unit> DestructionInstructionLiveData { get; }

        public void OnUpdateWorldConfig(WorldConfig worldConfiguration)
        {
            WorldConfigLiveData.PostValue(worldConfiguration);
        }

        public void OnUpdateCoordinate(Coordinate coordinate)
        {
            CoordinateLiveData.PostValue(coordinate);
        }

        public void OnUpdatePosition(Vector3 position)
        {
            PositionLiveData.PostValue(position);
        }

        public void OnSetCoordinateVisibility(bool visibility)
        {
            CoordinateVisibilityLiveData.PostValue(visibility);
        }

        public void OnSetLineVisibility(bool visibility)
        {
            LineVisibilityLiveData.PostValue(visibility);
        }

        public void OnReceivedDestructionInstruction()
        {
            DestructionInstructionLiveData.PostValue(Unit.Default);
        }
    }
}