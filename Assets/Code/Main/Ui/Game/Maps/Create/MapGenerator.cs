using System;
using Main.Core.Game.GameState.CurrentMapConfig;
using Main.Core.Game.GameState.CurrentWorldConfig;
using Main.Ui.Game.Maps.TilesPosition;
using Main.Ui.Game.Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.Maps.Create
{
    public class MapGenerator : MonoBehaviour
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;

        [FormerlySerializedAs("tileHolderProvider")] [FormerlySerializedAs("tileRepresentationProvider")] [SerializeField]
        private TileHolderFactory tileHolderFactory;

        [FormerlySerializedAs("tilesPositionProvider")] [SerializeField]
        private TilesPositionServiceProvider tilesPositionServiceProvider;

        [SerializeField] private Transform mapTransform;


        private IDisposable _disposable;

        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var worldConfigObservable = currentWorldConfigRepositoryProvider.Provide().GetMostRecent();
            var mapConfigObservable = currentMapConfigRepositoryProvider.Provide().GetMostRecent();
            var positionsObservable = tilesPositionServiceProvider.Provide().GetMostRecent(mapTransform.position.y);

            _disposable = mapConfigObservable
                .CombineLatest(
                    worldConfigObservable,
                    positionsObservable,
                    (mapConfig, worldConfig, positions) =>
                        (mapConfig, worldConfig, positions)
                )
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    tuple =>
                    {
                        var selfTransform = transform;
                        var (mapConfig, worldConfig, positions) = tuple;
                        for (var i = 0; i < mapConfig.GetMap2DActualHeight(); i++)
                        for (var j = 0; j < mapConfig.GetMap2DActualWidth(); j++)
                        {
                            var newTile = tileHolderFactory.Create().gameObject.transform;

                            newTile.parent = selfTransform;
                            newTile.rotation *= Quaternion.AngleAxis(30f, worldConfig.UpAxis);
                            newTile.position = positions[i * mapConfig.GetMap2DActualWidth() + j];
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