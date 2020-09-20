using System;
using Common.Holders;
using Constructs;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.BoardItemsHolder;
using GameEnvironments.Common.Repositories.BoardItemsHolder.Providers;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
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
        private IBoardItemsHolderRepository<TileHolder> _tileHoldersRepository;
        private IBoardItemsHolderRepository<UnitHolder> _unitHoldersRepository;
        private IBoardItemsHolderRepository<ConstructHolder> _constructHoldersRepository;
        private IBoardItemsHolderRepository<StrongholdHolder> _strongholdHoldersRepository;

        private void OnEnable()
        {
            _gameEnvironmentSetRepository = currentGameEnvironmentRepositoryProvider.Provide();
            _mapTransformGetRepository = mapTransformRepositoryProvider.Provide();
            _tileHoldersRepository = tilesHolderRepositoryProvider.Provide();
            _unitHoldersRepository = unitsHolderRepositoryProvider.Provide();
            _constructHoldersRepository = constructsHolderRepositoryProvider.Provide();
            _strongholdHoldersRepository = strongholdHolderRepositoryProvider.Provide();
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

            

            return GetRecycleHoldersObservable(_tileHoldersRepository)
                .Concat(GetRecycleHoldersObservable(_constructHoldersRepository))
                .Concat(GetRecycleHoldersObservable(_unitHoldersRepository))
                .Concat(GetRecycleHoldersObservable(_strongholdHoldersRepository))
                .Concat(clearAllGameObjects);
        }

        private IObservable<Unit> GetRecycleHoldersObservable<T>(IBoardItemsHolderRepository<T> holdersRepository) where T : IBoardItemHolder
        {
            return holdersRepository.GetMostRecent()
                .Select(
                    holders =>
                    {
                        foreach (var holder in holders)
                        {
                            holder.ReturnToPool();
                        }

                        return Unit.Default;
                    }
                );
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