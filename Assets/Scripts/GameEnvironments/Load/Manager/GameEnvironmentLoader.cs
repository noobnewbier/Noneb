using System;
using GameEnvironments.Load.BoardItemOnTile.Loaders;
using GameEnvironments.Load.BoardItemOnTile.StrongholdInternalPosition;
using GameEnvironments.Load.GameObjects.Loaders;
using GameEnvironments.Load.Tiles;
using Maps;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Manager
{
    public class GameEnvironmentLoader : MonoBehaviour
    {
        [SerializeField] private CurrentTilesTransformSetter currentTilesTransformSetter;
        [SerializeField] private CurrentMapTransformSetter currentMapTransformSetter;


        [SerializeField] private MapLoader mapLoader;

        [SerializeField] private UnitLoader unitLoader;
        [SerializeField] private ConstructLoader constructLoader;
        [SerializeField] private StrongholdLoader strongholdLoader;

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
                .Concat(LoadGameObjects());
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private IObservable<Unit> LoadPreliminaries()
        {
            currentTilesTransformSetter.Set();
            currentMapTransformSetter.Set();

            return Observable.ReturnUnit();
        }

        private IObservable<Unit> LoadMap()
        {
            return Observable.Defer(() => mapLoader.LoadObservable());
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
            );
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
            );
        }
    }
}