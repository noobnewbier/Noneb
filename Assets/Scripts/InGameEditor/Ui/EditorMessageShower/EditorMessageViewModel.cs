using System;
using Experiment.CrossPlatformLiveData;
using InGameEditor.Events;
using InGameEditor.Services.InGameEditorMessage;
using UniRx;

namespace InGameEditor.Ui.EditorMessageShower
{
    public interface IEditorMessageViewModel : IDisposable
    {
        LiveData<string> MessageLiveData { get; }
    }

    public class EditorMessageViewModel : IEditorMessageViewModel
    {
        private readonly IInGameEditorMessageService _messageService;
        private readonly IDisposable _disposable;

        public EditorMessageViewModel(IInGameEditorMessageService messageService)
        {
            _messageService = messageService;
            //something is weird with my liveData...
            MessageLiveData = new LiveData<string>();
            var observer = Observer.Create<InGameEditorUiMessage>(
                message => MessageLiveData.PostValue(message.Value)
            );

            _disposable = messageService.InGameEditorUiMessageStream.Subscribe(observer);
        }

        public LiveData<string> MessageLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
            _messageService.Dispose();
        }
    }
}