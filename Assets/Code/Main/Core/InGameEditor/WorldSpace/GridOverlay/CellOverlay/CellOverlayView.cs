using Maps;
using TMPro;
using UniRx;
using UnityEngine;
using UnityUtils.Pooling;
using WorldConfigurations;

namespace InGameEditor.WorldSpace.GridOverlay.CellOverlay
{
    public class CellOverlayView : PooledMonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private TextMeshPro coordinateText;

        private CompositeDisposable _compositeDisposable;

        public void Initialize(CellOverlayViewModel cellOverlayViewModel)
        {
            _compositeDisposable = new CompositeDisposable
            {
                cellOverlayViewModel.PositionLiveData.Subscribe(UpdatePosition),
                cellOverlayViewModel.WorldConfigLiveData.Subscribe(UpdateCellLineSettings),
                cellOverlayViewModel.WorldConfigLiveData.Subscribe(UpdateCoordinateTextSettings),
                cellOverlayViewModel.CoordinateVisibilityLiveData.Subscribe(SetTextVisibility),
                cellOverlayViewModel.LineVisibilityLiveData.Subscribe(SetLineVisibility),
                cellOverlayViewModel.CoordinateLiveData.Subscribe(UpdateCoordinateTextContent),
                cellOverlayViewModel.DestructionInstructionLiveData.Subscribe(_ => SelfDestroy())
            };
        }

        private void UpdateCoordinateTextSettings(WorldConfig worldConfiguration)
        {
            var rectTransform = coordinateText.GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, worldConfiguration.OuterRadius);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, worldConfiguration.InnerRadius);
        }

        private void UpdateCoordinateTextContent(Coordinate coordinate)
        {
            coordinateText.text = $"{coordinate.X},{coordinate.Y},{coordinate.Z}";
        }

        private void UpdateCellLineSettings(WorldConfig worldConfiguration)
        {
            lineRenderer.positionCount = worldConfiguration.TileCorners.Length;
            lineRenderer.SetPositions(worldConfiguration.TileCorners);
        }

        private void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetTextVisibility(bool visibility)
        {
            coordinateText.enabled = visibility;
        }

        private void SetLineVisibility(bool visibility)
        {
            lineRenderer.enabled = visibility;
        }

        private void SelfDestroy()
        {
            ReturnToPool();
        }

        private void OnDisable()
        {
            _compositeDisposable?.Dispose();
        }
    }
}