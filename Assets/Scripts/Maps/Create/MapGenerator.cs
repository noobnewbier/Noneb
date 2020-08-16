using System;
using Constructs;
using Maps.Repositories;
using Tiles.Holders;
using UniRx;
using Units.Holders;
using UnityEngine;
using UnityEngine.Serialization;
using WorldConfigurations.Repositories;

namespace Maps.Create
{
    public class MapGenerator : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float chanceOfConstructOnTile;
        [Range(0f, 1f)] [SerializeField] private float chanceOfUnitOnTile;
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;
        [SerializeField] private WorldConfigurationRepositoryProvider worldConfigurationRepositoryProvider;

        [FormerlySerializedAs("constructRepresentationProvider")] [SerializeField]
        private ConstructHolderProvider constructHolderProvider;

        [FormerlySerializedAs("tileRepresentationProvider")] [SerializeField]
        private TileHolderProvider tileHolderProvider;

        [SerializeField] private TilesPositionProvider tilesPositionProvider;

        [FormerlySerializedAs("unitHoldersProvider")] [FormerlySerializedAs("unitRepresentationProvider")] [SerializeField]
        private UnitHolderProvider unitHolderProvider;

        [SerializeField] private GameObject rowPrefab;

        private IDisposable _disposable;

        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var worldConfigObservable = worldConfigurationRepositoryProvider.Provide().Get();
            var mapConfigObservable = mapConfigurationRepositoryProvider.Provide().Get();

            _disposable = mapConfigObservable
                .Zip(
                    worldConfigObservable,
                    (mapConfig, worldConfig) =>
                        (mapConfig, worldConfig)
                )
                .Subscribe(
                    tuple =>
                    {
                        var selfTransform = transform;
                        var positions = tilesPositionProvider.Provide();
                        var (mapConfig, worldConfig) = tuple;
                        for (var i = 0; i < mapConfig.ZSize; i++)
                        {
                            var row = Instantiate(rowPrefab).transform;
                            row.parent = selfTransform;
                            for (var j = 0; j < mapConfig.XSize; j++)
                            {
                                var newTile = tileHolderProvider.Provide().GameObject.transform;

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