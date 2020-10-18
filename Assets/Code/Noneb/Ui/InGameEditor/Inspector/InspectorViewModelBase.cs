using System;
using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.InGameEditor.Common;
using UniRx;

namespace Noneb.Ui.InGameEditor.Inspector
{
    public abstract class InspectorViewModelBase<T> : IDisposable where T : BoardItemData
    {
        public ILiveData<T> TypeTLiveData { get; }
        public ILiveData<bool> VisibilityLiveData { get; }

        private readonly IDisposable _disposable;

        public InspectorViewModelBase(IDataGetRepository<IInspectable> currentInspectableGetRepository)
        {
            TypeTLiveData = new LiveData<T>();
            VisibilityLiveData = new LiveData<bool>();

            _disposable = currentInspectableGetRepository.GetObservableStream()
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(OnInspectableUpdate);
        }

        private void OnInspectableUpdate(IInspectable inspectable)
        {
            if (TryGetTFromInspectable(inspectable, out var t))
            {
                UpdateVisibility(true);
                UpdateInspectable(t);
            }
            else
            {
                UpdateVisibility(false);
            }
        }

        protected abstract bool TryGetTFromInspectable(IInspectable inspectable, out T t);

        private void UpdateInspectable(T tData)
        {
            TypeTLiveData.PostValue(tData);
        }

        private void UpdateVisibility(bool isVisible)
        {
            VisibilityLiveData.PostValue(isVisible);
        }

        public virtual void Dispose()
        {
            _disposable.Dispose();
        }
    }
}