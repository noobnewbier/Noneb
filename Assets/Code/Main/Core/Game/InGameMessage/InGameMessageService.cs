using System;
using InGameEditor.Events;
using UniRx;

namespace InGameEditor.Services.InGameEditorMessage
{
    public interface IInGameMessageService : IDisposable
    {
        ISubject<InGameMessage> InGameEditorUiMessageStream { get; }
        void PublishMessage(string message);
    }

    public class InGameMessageService : IInGameMessageService
    {
        private readonly Subject<InGameMessage> _inGameEditorUiMessageStream;

        public InGameMessageService()
        {
            _inGameEditorUiMessageStream = new Subject<InGameMessage>();
        }

        public ISubject<InGameMessage> InGameEditorUiMessageStream => _inGameEditorUiMessageStream;

        public void PublishMessage(string message)
        {
            _inGameEditorUiMessageStream.OnNext(new InGameMessage(message));
        }

        //not sure if we actually need this...
        public void Dispose()
        {
            _inGameEditorUiMessageStream.Dispose();
        }
    }
}