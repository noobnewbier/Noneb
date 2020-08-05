using System;
using Experiment.CrossPlatformLiveData;
using InGameEditor.Events;
using UniRx;

namespace InGameEditor.Services
{
    public interface IInGameEditorMessageService : IDisposable
    {
        ISubject<InGameEditorUiMessage> InGameEditorUiMessageStream { get; }
        void PublishMessage(string message);
    }

    public class InGameEditorMessageService : IInGameEditorMessageService
    {
        private readonly Subject<InGameEditorUiMessage> _inGameEditorUiMessageStream;

        public ISubject<InGameEditorUiMessage> InGameEditorUiMessageStream => _inGameEditorUiMessageStream;

        public InGameEditorMessageService()
        {
            _inGameEditorUiMessageStream = new Subject<InGameEditorUiMessage>();
        }

        public void PublishMessage(string message)
        {
            _inGameEditorUiMessageStream.OnNext(new InGameEditorUiMessage(message));
        }

        //not sure if we actually need this...
        public void Dispose()
        {
            _inGameEditorUiMessageStream.Dispose();
        }
    }
}