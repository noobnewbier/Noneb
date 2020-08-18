using Experiment.CrossPlatformLiveData;
using Maps;
using UniRx;
using UnityEngine;
using WorldConfigurations;

namespace InGameEditor.WorldSpace.GridOverlay.CellOverlay
{
    public class CellOverlayViewModel
    {
        public CellOverlayViewModel(Vector3 position,
                                    WorldConfig worldConfig,
                                    bool coordinateVisibility,
                                    bool lineVisibility,
                                    Coordinate coordinate)
        {
            PositionLiveData = new LiveData<Vector3>(position);
            WorldConfigLiveData = new LiveData<WorldConfig>(worldConfig);
            CoordinateVisibilityLiveData = new LiveData<bool>(coordinateVisibility);
            LineVisibilityLiveData = new LiveData<bool>(lineVisibility);
            CoordinateLiveData = new LiveData<Coordinate>(coordinate);
            DestructionInstructionLiveData = new LiveData<Unit>();
        }

        public ILiveData<Vector3> PositionLiveData { get; }
        public ILiveData<WorldConfig> WorldConfigLiveData { get; }
        public ILiveData<bool> CoordinateVisibilityLiveData { get; }
        public ILiveData<bool> LineVisibilityLiveData { get; }
        public ILiveData<Coordinate> CoordinateLiveData { get; }
        public ILiveData<Unit> DestructionInstructionLiveData { get; }

        public void OnUpdateWorldConfiguration(WorldConfig worldConfiguration)
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