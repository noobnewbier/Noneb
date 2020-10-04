using System;
using GameEnvironments.Common.Repositories.BoardItemsHolderProviders.Setters;
using GameEnvironments.Load.BoardItems.Loaders;
using GameEnvironments.Load.CleanUp.StrongholdInternalPosition;
using GameEnvironments.Load.GameObjects.Loaders;
using GameEnvironments.Load.Holders.Loaders;
using InGameEditor.Services.InGameEditorMessage;
using Maps;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Manager
{
    public class GameEnvironmentLoader : MonoBehaviour
    {
        private IDisposable _disposable;

        private void OnEnable()
        {
            _messageService = messageServiceProvider.Provide();
        }

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            _disposable = GetLoadNonGameObjectRelatedObservable()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Concat(GetGameObjectRelatedLoadObservable())
                .Last()
                .Subscribe(
                    _ =>
                    {
                        Debug.Log("success");
                        Debug.Log("success");
                    },
                    e =>
                    {
#if UNITY_EDITOR
                        _messageService.PublishMessage(e.ToString());
#else
                        throw e;
#endif
                    }
                );
        }

        public IObservable<Unit> GetGameObjectRelatedLoadObservable() =>
            LoadBoardItemHolders()
                .Concat(LoadGameObjects())
                .Concat(CleanUp())
                .Last();

        public IObservable<Unit> GetLoadNonGameObjectRelatedObservable() =>
            LoadPreliminaries()
                .Concat(LoadBoardItems())
                .Last();

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private IObservable<Unit> LoadPreliminaries()
        {
            currentMapTransformSetter.Set();

            unitsHolderFetcherSetter.Set();
            tilesFetcherSetter.Set();
            constructsFetcherSetter.Set();
            strongholdsFetcherSetter.Set();

            return Observable.ReturnUnit().Single();
        }

        private IObservable<Unit> LoadBoardItems()
        {
            return Observable.Defer(
                    () => tilesLoader.LoadObservable()
                        .Zip(
                            unitsLoader.LoadObservable(),
                            constructsLoader.LoadObservable(),
                            strongholdsLoader.LoadObservable(),
                            delegate { return Unit.Default; }
                        )
                )
                .Single();
        }

        private IObservable<Unit> LoadBoardItemHolders()
        {
            return Observable.Defer(
                    () => tileHoldersLoader.LoadObservable()
                        .Zip(
                            unitHoldersLoader.LoadObservable(),
                            constructHoldersLoader.LoadObservable(),
                            strongholdHoldersLoader.LoadObservable(),
                            delegate { return Unit.Default; }
                        )
                )
                .Single();
        }

        private IObservable<Unit> LoadGameObjects()
        {
            return Observable.Defer(
                    () => tileGameObjectLoader.LoadObservable()
                        .Zip(
                            unitGameObjectLoader.LoadObservable(),
                            constructGameObjectLoader.LoadObservable(),
                            strongholdUnitGameObjectLoader.LoadObservable(),
                            strongholdConstructGameObjectLoader.LoadObservable(),
                            delegate { return Unit.Default; }
                        )
                )
                .Single();
        }

        private IObservable<Unit> CleanUp()
        {
            return Observable.Defer(
                    () =>
                        strongholdGameObjectsInternalPositionLoader.LoadObservable()
                )
                .Single();
        }

        #region Prelimainaries

        [SerializeField] private CurrentMapTransformSetter currentMapTransformSetter;
        [SerializeField] private UnitsHolderFetcherSetter unitsHolderFetcherSetter;
        [SerializeField] private ConstructsHolderFetcherSetter constructsFetcherSetter;
        [SerializeField] private StrongholdsHolderFetcherSetter strongholdsFetcherSetter;
        [SerializeField] private TilesHolderFetcherSetter tilesFetcherSetter;

        #endregion

        #region DataLoaders

        [SerializeField] private UnitsLoader unitsLoader;
        [SerializeField] private ConstructsLoader constructsLoader;
        [SerializeField] private StrongholdsLoader strongholdsLoader;
        [SerializeField] private TilesLoader tilesLoader;

        #endregion

        #region HolderLoaders

        [SerializeField] private UnitHoldersLoader unitHoldersLoader;
        [SerializeField] private ConstructHoldersLoader constructHoldersLoader;
        [SerializeField] private StrongholdHoldersLoader strongholdHoldersLoader;
        [SerializeField] private TileHoldersLoader tileHoldersLoader;

        #endregion

        #region GameObjectLoaders

        [SerializeField] private TileGameObjectLoader tileGameObjectLoader;
        [SerializeField] private UnitGameObjectLoader unitGameObjectLoader;
        [SerializeField] private ConstructGameObjectLoader constructGameObjectLoader;
        [SerializeField] private StrongholdUnitGameObjectLoader strongholdUnitGameObjectLoader;
        [SerializeField] private StrongholdConstructGameObjectLoader strongholdConstructGameObjectLoader;

        #endregion

        #region CleanUp

        [SerializeField] private StrongholdGameObjectsInternalPositionLoader strongholdGameObjectsInternalPositionLoader;
        [SerializeField] private InGameMessageServiceProvider messageServiceProvider;

        private IInGameMessageService _messageService;

        #endregion
    }
}