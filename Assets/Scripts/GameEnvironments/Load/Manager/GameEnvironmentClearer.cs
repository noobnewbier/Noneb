using System;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using Maps.Repositories.CurrentMapTransform;
using Tiles.Holders.Repository;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Manager
{
    /// <summary>
    /// We clear the scene by loading empty data
    /// </summary>
    public class GameEnvironmentClearer : MonoBehaviour
    {
        [SerializeField] private GameEnvironmentLoader gameEnvironmentLoader;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;
        [SerializeField] private TileHoldersRepositoryProvider tileHoldersRepositoryProvider;
        [SerializeField] private CurrentMapTransformRepositoryProvider mapTransformRepositoryProvider;

        private IDisposable _disposable;
        private ICurrentGameEnvironmentSetRepository _gameEnvironmentSetRepository;
        private ICurrentMapTransformGetRepository _mapTransformGetRepository;
        private ITileHoldersRepository _tileHoldersRepository;

        private void OnEnable()
        {
            _gameEnvironmentSetRepository = currentGameEnvironmentRepositoryProvider.Provide();
            _tileHoldersRepository = tileHoldersRepositoryProvider.Provide();
            _mapTransformGetRepository = mapTransformRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Clear))]
        public void Clear()
        {
            _disposable?.Dispose();

            _disposable = ClearGameObjects()
                .Concat(LoadEmptyEnvironment())
                .Last()
                .SubscribeOn(Scheduler.MainThread) //todo: use proper threading
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    _ =>
                    {
                        //todo: proper error handling
                        Debug.Log("level cleared");
                    }
                );
        }

        private IObservable<Unit> ClearGameObjects()
        {
            var clearAllGameObjects = _mapTransformGetRepository.GetMostRecent()
                .Select(
                    map =>
                    {
                        foreach (Transform child in map)
                            if (child != map)
                            {
                                Destroy(child.gameObject);
                            }

                        return Unit.Default;
                    }
                )
                .Single();

            var recycleAllHolders = _tileHoldersRepository
                .GetAllFlattenSingle()
                .Select(
                    tiles =>
                    {
                        foreach (var tile in tiles) tile.ReturnToPool();

                        return Unit.Default;
                    }
                )
                .Single();

            return recycleAllHolders.Concat(clearAllGameObjects);
        }

        private IObservable<Unit> LoadEmptyEnvironment()
        {
            _gameEnvironmentSetRepository.Set(GameEnvironment.Empty);

            return gameEnvironmentLoader.GetLoadObservable().Single();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}