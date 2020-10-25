using System;
using Noneb.Core.Game.InGameMessages;
using Noneb.Ui.Game.GameEnvironments.Load.BoardItems.Loaders;
using Noneb.Ui.Game.GameEnvironments.Load.CleanUp.StrongholdInternalPosition;
using Noneb.Ui.Game.GameEnvironments.Load.GameObjects.Loaders;
using Noneb.Ui.Game.GameEnvironments.Load.Holders.Loaders;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Setters;
using Noneb.Ui.Game.UiState.CurrentMapTransform;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.Manager
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
                    _ => { _messageService.PublishMessage("success"); },
                    e =>
                    {
#if UNITY_EDITOR
                        throw e;
#else
                        _messageService.PublishMessage(e.ToString());
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
            LoadBoardItems();

        private void OnDisable()
        {
            _disposable?.Dispose();
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