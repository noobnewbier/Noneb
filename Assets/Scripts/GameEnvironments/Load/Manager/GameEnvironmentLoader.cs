using System;
using GameEnvironments.Load.BoardItems.Loaders;
using GameEnvironments.Load.CleanUp.StrongholdInternalPosition;
using GameEnvironments.Load.GameObjects.Loaders;
using GameEnvironments.Load.Obsolete.BoardItemOnTile.StrongholdInternalPosition;
using Maps;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.Manager
{
    public class GameEnvironmentLoader : MonoBehaviour
    {
        [SerializeField] private CurrentTilesTransformSetter currentTilesTransformSetter;
        [SerializeField] private CurrentMapTransformSetter currentMapTransformSetter;

        [FormerlySerializedAs("mapLoader")] [SerializeField]
        private TilesLoader tileLoader;
        [SerializeField] private UnitsLoader unitLoader;
        [SerializeField] private ConstructsLoader constructLoader;
        [SerializeField] private StrongholdsLoader strongholdLoader;

        [SerializeField] private TileGameObjectLoader tileGameObjectLoader;
        [SerializeField] private UnitGameObjectLoader unitGameObjectLoader;
        [SerializeField] private ConstructGameObjectLoader constructGameObjectLoader;
        [SerializeField] private StrongholdUnitGameObjectLoader strongholdUnitGameObjectLoader;
        [SerializeField] private StrongholdConstructGameObjectLoader strongholdConstructGameObjectLoader;
        [SerializeField] private StrongholdGameObjectsInternalPositionLoader strongholdGameObjectsInternalPositionLoader;

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
                .Concat(LoadMap())
                .Concat(LoadBoardItemOnTileHolders())
                .Concat(LoadGameObjects())
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

        private IObservable<Unit> LoadMap()
        {
            return Observable.Defer(() => tileLoader.LoadObservable()).Single();
        }

        private IObservable<Unit> LoadBoardItemOnTileHolders()
        {
            return Observable.Defer(
                    () => unitLoader.LoadObservable()
                        .Zip(
                            constructLoader.LoadObservable(),
                            strongholdLoader.LoadObservable(),
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
                            strongholdGameObjectsInternalPositionLoader.LoadObservable(),
                            delegate { return Unit.Default; }
                        )
                )
                .Single();
        }
    }
}