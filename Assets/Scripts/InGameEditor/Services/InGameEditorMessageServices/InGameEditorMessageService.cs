using System;
using InGameEditor.Events;
using UniRx;

namespace InGameEditor.Services.InGameEditorMessageServices
{
    public interface IInGameEditorMessageService : IDisposable
    {
        ISubject<InGameEditorUiMessage> InGameEditorUiMessageStream { get; }
        void PublishMessage(string message);
    }

    public class InGameEditorMessageService : IInGameEditorMessageService
    {
        private readonly Subject<InGameEditorUiMessage> _inGameEditorUiMessageStream;

        public InGameEditorMessageService()
        {
            _inGameEditorUiMessageStream = new Subject<InGameEditorUiMessage>();
        }

        public ISubject<InGameEditorUiMessage> InGameEditorUiMessageStream => _inGameEditorUiMessageStream;

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