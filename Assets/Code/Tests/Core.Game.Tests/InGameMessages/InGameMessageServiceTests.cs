using Noneb.Core.Game.InGameMessages;
using NUnit.Framework;
using UniRx;

namespace Core.Game.Tests.InGameMessages
{
    [TestFixture]
    public class InGameMessageServiceTests
    {
        private InGameMessageService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new InGameMessageService();
        }

        [Test]
        public void WhenPublishMessage_ObserverIsUpdated()
        {
            const string expectedMessage = "random noob message";
            InGameMessage observedMessage;
            _service.InGameEditorUiMessageStream
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(v => observedMessage = v);
            _service.PublishMessage(expectedMessage);

            Assert.That(observedMessage.Value, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void WhenPublishMessageBeforeSubscribed_ObserverIsNotUpdated()
        {
            const string sentMessage = "random noob message";
            _service.PublishMessage(sentMessage);
            
            InGameMessage observedMessage;
            _service.InGameEditorUiMessageStream
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(v => observedMessage = v);

            Assert.That(observedMessage.Value, Is.Null.And.Not.EqualTo(sentMessage));
        }

        [Test]
        public void WhenPublishMessageWithEmptyString_ThrowException()
        {
            void PublishEmptyMessage()
            {
                _service.PublishMessage(string.Empty);
            }
            
            Assert.That(PublishEmptyMessage, Throws.ArgumentException);
        }
        
        [Test]
        public void WhenPublishMessageWithNull_ThrowException()
        {
            void PublishEmptyMessage()
            {
                _service.PublishMessage(null);
            }
            
            Assert.That(PublishEmptyMessage, Throws.ArgumentException);
        }
    }
}