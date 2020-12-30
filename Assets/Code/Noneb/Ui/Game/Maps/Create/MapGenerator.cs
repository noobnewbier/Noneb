using System;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.GameState.WorldConfig;
using Noneb.Ui.Game.Maps.TilesPosition;
using Noneb.Ui.Game.Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.Game.Maps.Create
{
    public class MapGenerator : MonoBehaviour
    {
        [FormerlySerializedAs("currentMapConfigRepositoryProvider")] [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private MapConfigRepositoryProvider selectedMapConfigRepositoryProvider;

        [FormerlySerializedAs("currentWorldConfigRepositoryProvider")]
        [FormerlySerializedAs("worldConfigRepositoryProvider")]
        [FormerlySerializedAs("worldConfigurationRepositoryProvider")]
        [SerializeField]
        private WorldConfigRepositoryProvider selectedWorldConfigRepositoryProvider;

        [FormerlySerializedAs("tileHolderProvider")] [FormerlySerializedAs("tileRepresentationProvider")] [SerializeField]
        private TileHolderFactory tileHolderFactory;

        [FormerlySerializedAs("tilesPositionProvider")] [SerializeField]
        private TilesPositionServiceProvider tilesPositionServiceProvider;

        [SerializeField] private Transform mapTransform;


        private IDisposable _disposable;

        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var worldConfigObservable = selectedWorldConfigRepositoryProvider.Provide().GetMostRecent();
            var mapConfigObservable = selectedMapConfigRepositoryProvider.Provide().GetMostRecent();
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