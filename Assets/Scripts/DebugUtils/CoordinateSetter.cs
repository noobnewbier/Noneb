using System;
using Common;
using Maps;
using Maps.Repositories;
using Tiles;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace DebugUtils
{
    /// <summary>
    /// Assuming attached to parent of all hexagons' rows, which contains the tiles
    /// </summary>
    public class CoordinateSetter : MonoBehaviour
    {
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        private IDisposable _disposable;

        /// <summary>
        /// Assuming it's a rectangle, reference: https://www.redblobgames.com/grids/hexagons/#map-storage map-storage section
        /// </summary>
        [ContextMenu("SetCoordinates")]
        private void SetCoordinates()
        {
            var tilesTransform = tilesTransformProvider.Provide();
            _disposable = mapConfigurationRepositoryProvider.Provide()
                .Get()
                .Subscribe(
                    config =>
                    {
                        Assert.AreEqual(
                            tilesTransform.Count,
                            config.ZSize * config.XSize,
                            "Number of tile representation is different from the configuration"
                        );

                        for (var i = 0; i < config.ZSize; i++)
                        for (var j = 0; j < config.XSize; j++)
                        {
                            var hexTransform = tilesTransform[i * config.XSize + j];
                            var requireCoordinates = hexTransform.GetComponentsInChildren<IRequireCoordinate>();
                            var x = j + i % 2 + i / 2;
                            var z = i;
                            var coordinate = new Coordinate(x, z);

                            foreach (var requireCoordinate in requireCoordinates)
                            {
                                requireCoordinate.Coordinate = coordinate;
                            }
                        }
                    }
                );
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}