using System;
using Main.Core.Game.Common;
using Main.Core.Game.Coordinate;
using Main.Core.Game.GameState.CurrentMapConfig;
using Main.Ui.Game.Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace DebugUtils
{
    /// <summary>
    /// Assuming attached to parent of all hexagons' rows, which contains the tiles
    /// </summary>
    public class CoordinateSetter : MonoBehaviour
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("tilesHolderProvider")] [FormerlySerializedAs("tilesTransformProvider")] [SerializeField]
        private TilesHolderFetcher tilesHolderFetcher;

        private IDisposable _disposable;

        /// <summary>
        /// Assuming it's a rectangle, reference: https://www.redblobgames.com/grids/hexagons/#map-storage map-storage section
        /// </summary>
        [ContextMenu("SetCoordinates")]
        private void SetCoordinates()
        {
            var tilesTransform = tilesHolderFetcher.Fetch();
            _disposable = currentMapConfigRepositoryProvider.Provide()
                .GetMostRecent()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    config =>
                    {
                        Assert.AreEqual(
                            tilesTransform.Count,
                            config.GetMap2DActualHeight() * config.GetMap2DActualWidth(),
                            "Number of tile representation is different from the configuration"
                        );

                        for (var i = 0; i < config.GetMap2DActualHeight(); i++)
                        for (var j = 0; j < config.GetMap2DActualWidth(); j++)
                        {
                            var hexTransform = tilesTransform[i * config.GetMap2DActualWidth() + j];
                            var requireCoordinates = hexTransform.GetComponentsInChildren<IRequireCoordinate>();
                            var x = j + i % 2 + i / 2;
                            var z = i;
                            var coordinate = new Coordinate(x, z);

                            foreach (var requireCoordinate in requireCoordinates) requireCoordinate.Coordinate = coordinate;
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