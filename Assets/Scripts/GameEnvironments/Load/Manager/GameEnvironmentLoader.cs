using System;
using GameEnvironments.Load.BoardItems.Loaders;
using GameEnvironments.Load.CleanUp.StrongholdInternalPosition;
using GameEnvironments.Load.GameObjects.Loaders;
using GameEnvironments.Load.Holders.Loaders;
using Maps;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Manager
{
    public class GameEnvironmentLoader : MonoBehaviour
    {
        #region Prelimainaries

        [SerializeField] private CurrentTilesTransformSetter currentTilesTransformSetter;
        [SerializeField] private CurrentMapTransformSetter currentMapTransformSetter;

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

        #endregion


        private IDisposable _disposable;

        [ContextMenu(nameof(Load))]
        public void Load()
        {
            _disposable = GetLoadObservable()
                .SubscribeOn(Scheduler.MainThread) //todo: use proper threading
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    _ =>
                    {
                        //todo: proper error handling
                        Debug.Log("success");
                    }
                );
        }

        public IObservable<Unit> GetLoadObservable()
        {
            return LoadPreliminaries()
                .Concat(LoadBoardItems())
                .Concat(LoadBoardItemHolders())
                .Concat(LoadGameObjects())
                .Concat(CleanUp())
                .Last();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private IObservable<Unit> LoadPreliminaries()
        {
            currentTilesTransformSetter.Set();
            currentMapTransformSetter.Set();

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
    }
}