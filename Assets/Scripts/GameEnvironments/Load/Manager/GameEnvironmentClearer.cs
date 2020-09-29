using System;
using Common.Holders;
using Constructs;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.BoardItemsHolders;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using Maps.Repositories.CurrentMapTransform;
using Strongholds;
using Tiles.Holders;
using UniRx;
using Units.Holders;
using UnityEngine;

namespace GameEnvironments.Load.Manager
{
    /// <summary>
    /// todo: it won't work now, you just made it compile :)
    /// We clear the scene by loading empty data
    /// </summary>
    public class GameEnvironmentClearer : MonoBehaviour
    {
        [SerializeField] private GameEnvironmentLoader gameEnvironmentLoader;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        [SerializeField] private CurrentMapTransformRepositoryProvider mapTransformRepositoryProvider;

        [SerializeField] private TilesHolderRepositoryProvider tilesHolderRepositoryProvider;
        [SerializeField] private UnitsHolderRepositoryProvider unitsHolderRepositoryProvider;
        [SerializeField] private ConstructsHolderRepositoryProvider constructsHolderRepositoryProvider;
        [SerializeField] private StrongholdsHolderRepositoryProvider strongholdHolderRepositoryProvider;

        private IDisposable _disposable;
        private ICurrentGameEnvironmentSetRepository _gameEnvironmentSetRepository;
        private ICurrentMapTransformGetRepository _mapTransformGetRepository;
        private IBoardItemsHolderGetRepository<TileHolder> _tileHoldersGetRepository;
        private IBoardItemsHolderGetRepository<UnitHolder> _unitHoldersGetRepository;
        private IBoardItemsHolderGetRepository<ConstructHolder> _constructHoldersGetRepository;
        private IBoardItemsHolderGetRepository<StrongholdHolder> _strongholdHoldersGetRepository;

        private void OnEnable()
        {
            _gameEnvironmentSetRepository = currentGameEnvironmentRepositoryProvider.Provide();
            _mapTransformGetRepository = mapTransformRepositoryProvider.Provide();
            _tileHoldersGetRepository = tilesHolderRepositoryProvider.Provide();
            _unitHoldersGetRepository = unitsHolderRepositoryProvider.Provide();
            _constructHoldersGetRepository = constructsHolderRepositoryProvider.Provide();
            _strongholdHoldersGetRepository = strongholdHolderRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Clear))]
        public void Clear()
        {
            _disposable?.Dispose();

            _disposable = SelectEmptyEnvironment()
                .Concat(gameEnvironmentLoader.GetLoadNonGameObjectRelatedObservable())
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Concat(ClearGameObjects())
                .Concat(
                    Observable.NextFrame()//wait till next frame as it is possible that the game objects are not properly destroyed
                        .SelectMany(_ => gameEnvironmentLoader.GetGameObjectRelatedLoadObservable())
                )
                .Last()
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
                            Destroy(child.gameObject);

                        return Unit.Default;
                    }
                )
                .Single();


            return GetRecycleHoldersObservable(_tileHoldersGetRepository)
                .Concat(GetRecycleHoldersObservable(_constructHoldersGetRepository))
                .Concat(GetRecycleHoldersObservable(_unitHoldersGetRepository))
                .Concat(GetRecycleHoldersObservable(_strongholdHoldersGetRepository))
                .Concat(clearAllGameObjects)
                .Last();
        }

        private IObservable<Unit> GetRecycleHoldersObservable<T>(IBoardItemsHolderGetRepository<T> holdersGetRepository) where T : IBoardItemHolder
        {
            return holdersGetRepository.GetMostRecent()
                .Select(
                    holders =>
                    {
                        foreach (var holder in holders) holder.ReturnToPool();

                        return Unit.Default;
                    }
                );
        }

        private IObservable<Unit> SelectEmptyEnvironment()
        {
            _gameEnvironmentSetRepository.Set(GameEnvironment.Empty);

            return Observable.ReturnUnit();
        }


        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}