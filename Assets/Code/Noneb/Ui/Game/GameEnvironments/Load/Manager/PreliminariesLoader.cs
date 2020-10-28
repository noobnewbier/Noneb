using System;
using Noneb.Core.Game.InGameMessages;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Setters;
using Noneb.Ui.Game.UiState.CurrentGraphicRaycaster;
using Noneb.Ui.Game.UiState.CurrentMapTransform;
using Noneb.Ui.Game.UiState.CurrentUnityEventSystem;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.Manager
{
    public class PreliminariesLoader : MonoBehaviour
    {
        [SerializeField] private CurrentMapTransformSetter currentMapTransformSetter;
        [SerializeField] private UnitsHolderFetcherSetter unitsHolderFetcherSetter;
        [SerializeField] private ConstructsHolderFetcherSetter constructsFetcherSetter;
        [SerializeField] private StrongholdsHolderFetcherSetter strongholdsFetcherSetter;
        [SerializeField] private TilesHolderFetcherSetter tilesFetcherSetter;
        [SerializeField] private UnityEventSystemSetter unityEventSystemSetter;
        [SerializeField] private CurrentGraphicRaycasterSetter graphicRaycasterSetter;
        
        [SerializeField] private InGameMessageServiceProvider messageServiceProvider;


        private IDisposable _disposable;

        private void OnEnable()
        {
            LoadPreliminaries()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    _ => { messageServiceProvider.Provide().PublishMessage("preliminaries loaded"); },
                    e =>
                    {
#if UNITY_EDITOR
                        throw e;
#else
                        messageServiceProvider.Provide().PublishMessage(e.ToString());
#endif
                    }
                );
        }

        private IObservable<Unit> LoadPreliminaries()
        {
            currentMapTransformSetter.Set();

            unitsHolderFetcherSetter.Set();
            tilesFetcherSetter.Set();
            constructsFetcherSetter.Set();
            strongholdsFetcherSetter.Set();
            
            graphicRaycasterSetter.Set();
            unityEventSystemSetter.Set();

            return Observable.ReturnUnit().Single();
        }
    }
}