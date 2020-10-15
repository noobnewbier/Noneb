using System;
using Castle.Core.Internal;
using UniRx;

namespace Noneb.Core.Game.InGameMessages
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
            if (message.IsNullOrEmpty())
            {
                throw new ArgumentException("message cannot be null or empty");
            }
            
            _inGameEditorUiMessageStream.OnNext(new InGameMessage(message));
        }

        //not sure if we actually need this...
        public void Dispose()
        {
            _inGameEditorUiMessageStream.Dispose();
        }
    }
}