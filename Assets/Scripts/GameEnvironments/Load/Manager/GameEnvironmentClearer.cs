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
using UnityEngine.Serialization;

namespace GameEnvironments.Load.Manager
{
    /// <summary>
    /// We clear the scene by loading empty data
    /// </summary>
    public class GameEnvironmentClearer : MonoBehaviour
    {
        [SerializeField] private GameEnvironmentLoader gameEnvironmentLoader;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        [SerializeField] private CurrentMapTransformRepositoryProvider mapTransformRepositoryProvider;

        [FormerlySerializedAs("tileHolderssFetchingServiceProvider")] [FormerlySerializedAs("tilesHolderRepositoryProvider")] [SerializeField] private TileHoldersFetchingServiceProvider tileHoldersFetchingServiceProvider;
        [FormerlySerializedAs("unitsHolderRepositoryProvider")] [SerializeField] private UnitHoldersFetchingServiceProvider unitHoldersFetchingServiceProvider;
        [FormerlySerializedAs("constructsHolderRepositoryProvider")] [SerializeField] private ConstructsHoldersFetchingServiceProvider constructsHoldersFetchingServiceProvider;
        [FormerlySerializedAs("strongholdFetchingServiceRepositoryProvider")] [FormerlySerializedAs("strongholdHolderRepositoryProvider")] [SerializeField] private StrongholdHoldersFetchingServiceRepositoryProvider strongholdHolderFetchingServiceRepositoryProvider;

        private IDisposable _disposable;
        private ICurrentGameEnvironmentSetRepository _gameEnvironmentSetRepository;
        private ICurrentMapTransformGetRepository _mapTransformGetRepository;
        private IBoardItemHoldersFetchingService<TileHolder> _tileHoldersFetchingService;
        private IBoardItemHoldersFetchingService<UnitHolder> _unitHoldersFetchingService;
        private IBoardItemHoldersFetchingService<ConstructHolder> _constructHoldersFetchingService;
        private IBoardItemHoldersFetchingService<StrongholdHolder> _strongholdHoldersFetchingService;

        private void Initialize()
        {
            _gameEnvironmentSetRepository = currentGameEnvironmentRepositoryProvider.Provide();
            _mapTransformGetRepository = mapTransformRepositoryProvider.Provide();
            _tileHoldersFetchingService = tileHoldersFetchingServiceProvider.Provide();
            _unitHoldersFetchingService = unitHoldersFetchingServiceProvider.Provide();
            _constructHoldersFetchingService = constructsHoldersFetchingServiceProvider.Provide();
            _strongholdHoldersFetchingService = strongholdHolderFetchingServiceRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Clear))]
        public void Clear()
        {
            _disposable?.Dispose();
            
            Initialize();
            
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
            return GetRecycleHoldersObservable(_tileHoldersFetchingService)
                .Concat(GetRecycleHoldersObservable(_constructHoldersFetchingService))
                .Concat(GetRecycleHoldersObservable(_unitHoldersFetchingService))
                .Concat(GetRecycleHoldersObservable(_strongholdHoldersFetchingService))
                .Last();
        }

        private IObservable<Unit> GetRecycleHoldersObservable<T>(IBoardItemHoldersFetchingService<T> holdersFetchingService) where T : IBoardItemHolder
        {
            return holdersFetchingService.Fetch()
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