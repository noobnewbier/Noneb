using System;
using EventManagement;
using Experiment.CrossPlatformLiveData;
using InGameEditor.Events;

namespace InGameEditor.Ui.EditorMessageShower
{
    public interface IEditorMessageViewModel : IDisposable, IHandle<UiMessageEvent>
    {
        LiveData<string> MessageLiveData { get; }
    }

    public class EditorMessageViewModel : IEditorMessageViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        
        public EditorMessageViewModel(IEventAggregator uiEventAggregator)
        {
            MessageLiveData = new LiveData<string>();
            _eventAggregator = uiEventAggregator;
            
            _eventAggregator.Subscribe(this);
        }

        public LiveData<string> MessageLiveData { get; }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(UiMessageEvent @event)
        {
            MessageLiveData.PostValue(@event.Message);
        }
    }
}