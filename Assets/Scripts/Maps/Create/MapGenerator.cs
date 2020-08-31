using System;
using Maps.Repositories;
using Maps.Services;
using Tiles.Holders;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using WorldConfigurations.Repositories;

namespace Maps.Create
{
    public class MapGenerator : MonoBehaviour
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("worldConfigRepositoryProvider")] [FormerlySerializedAs("worldConfigurationRepositoryProvider")] [SerializeField]
        private CurrentWorldConfigRepositoryProvider currentWorldConfigRepositoryProvider;

        [FormerlySerializedAs("tileRepresentationProvider")] [SerializeField]
        private TileHolderProvider tileHolderProvider;

        [FormerlySerializedAs("tilesPositionProvider")] [SerializeField]
        private TilesPositionServiceProvider tilesPositionServiceProvider;

        [SerializeField] private GameObject rowPrefab;
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
                .Subscribe(
                    tuple =>
                    {
                        var selfTransform = transform;
                        var (mapConfig, worldConfig, positions) = tuple;
                        for (var i = 0; i < mapConfig.ZSize; i++)
                        {
                            var row = Instantiate(rowPrefab).transform;
                            row.parent = selfTransform;
                            for (var j = 0; j < mapConfig.XSize; j++)
                            {
                                var newTile = tileHolderProvider.Provide().gameObject.transform;

                                newTile.parent = row;
                                newTile.rotation *= Quaternion.AngleAxis(30f, worldConfig.UpAxis);
                                newTile.position = positions[i * mapConfig.XSize + j];
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