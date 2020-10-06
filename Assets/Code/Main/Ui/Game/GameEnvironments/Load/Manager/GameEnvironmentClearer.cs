using System;
using Main.Core.Game.GameEnvironments.CurrentGameEnvironments;
using Main.Core.Game.GameEnvironments.Data;
using Main.Core.Game.InGameMessage;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.Constructs;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using Main.Ui.Game.Strongholds;
using Main.Ui.Game.Tiles;
using Main.Ui.Game.Units;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.GameEnvironments.Load.Manager
{
    /// <summary>
    /// We clear the scene by loading empty data
    /// </summary>
    public class GameEnvironmentClearer : MonoBehaviour
    {
        [SerializeField] private GameEnvironmentLoader gameEnvironmentLoader;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;
        [SerializeField] private InGameMessageServiceProvider messageServiceProvider;

        [FormerlySerializedAs("tileHolderssFetchingServiceProvider")] [FormerlySerializedAs("tilesHolderRepositoryProvider")] [SerializeField]
        private TileHoldersFetchingServiceProvider tileHoldersFetchingServiceProvider;

        [FormerlySerializedAs("unitsHolderRepositoryProvider")] [SerializeField]
        private UnitHoldersFetchingServiceProvider unitHoldersFetchingServiceProvider;

        [FormerlySerializedAs("constructsHolderRepositoryProvider")] [SerializeField]
        private ConstructsHoldersFetchingServiceProvider constructsHoldersFetchingServiceProvider;

        [FormerlySerializedAs("strongholdHolderFetchingServiceRepositoryProvider")]
        [FormerlySerializedAs("strongholdFetchingServiceRepositoryProvider")]
        [FormerlySerializedAs("strongholdHolderRepositoryProvider")]
        [SerializeField]
        private StrongholdHoldersFetchingServiceProvider strongholdHolderFetchingServiceProvider;

        private IDisposable _disposable;
        private ICurrentGameEnvironmentSetRepository _gameEnvironmentSetRepository;
        private IBoardItemHoldersFetchingService<TileHolder> _tileHoldersFetchingService;
        private IBoardItemHoldersFetchingService<UnitHolder> _unitHoldersFetchingService;
        private IBoardItemHoldersFetchingService<ConstructHolder> _constructHoldersFetchingService;
        private IBoardItemHoldersFetchingService<StrongholdHolder> _strongholdHoldersFetchingService;
        private IInGameMessageService _messageService;

        private void Initialize()
        {
            _gameEnvironmentSetRepository = currentGameEnvironmentRepositoryProvider.Provide();
            _tileHoldersFetchingService = tileHoldersFetchingServiceProvider.Provide();
            _unitHoldersFetchingService = unitHoldersFetchingServiceProvider.Provide();
            _constructHoldersFetchingService = constructsHoldersFetchingServiceProvider.Provide();
            _strongholdHoldersFetchingService = strongholdHolderFetchingServiceProvider.Provide();
            _messageService = messageServiceProvider.Provide();
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
                    Observable.NextFrame() //wait till next frame as it is possible that the game objects are not properly destroyed
                        .SelectMany(_ => gameEnvironmentLoader.GetGameObjectRelatedLoadObservable())
                )
                .Last()
                .Subscribe(
                    _ => { Debug.Log("level cleared"); },
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

        private IObservable<Unit> ClearGameObjects() =>
            GetRecycleHoldersObservable(_tileHoldersFetchingService)
                .Concat(GetRecycleHoldersObservable(_constructHoldersFetchingService))
                .Concat(GetRecycleHoldersObservable(_unitHoldersFetchingService))
                .Concat(GetRecycleHoldersObservable(_strongholdHoldersFetchingService))
                .Last();

        private IObservable<Unit> GetRecycleHoldersObservable<T>(IBoardItemHoldersFetchingService<T> holdersFetchingService)
            where T : IBoardItemHolder
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