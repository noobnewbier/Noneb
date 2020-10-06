using System;
using Experiment.CrossPlatformLiveData;
using Main.Core.Game.InGameMessage;
using UniRx;

namespace Main.Ui.Game.InGameMessage.DebugLogMessageShower
{
    public interface IDebugLogMessageViewModel : IDisposable
    {
        LiveData<string> MessageLiveData { get; }
    }

    public class DebugLogMessageViewModel : IDebugLogMessageViewModel
    {
        private readonly IInGameMessageService _messageService;
        private readonly IDisposable _disposable;

        public DebugLogMessageViewModel(IInGameMessageService messageService)
        {
            _messageService = messageService;
            MessageLiveData = new LiveData<string>();

            _disposable = messageService.InGameEditorUiMessageStream
                .SubscribeOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(
                    message => MessageLiveData.PostValue(message.Value)
                );
        }

        public LiveData<string> MessageLiveData { get; }

        public void Dispose()
        {
            _disposable.Dispose();
            _messageService.Dispose();
        }
    }
}