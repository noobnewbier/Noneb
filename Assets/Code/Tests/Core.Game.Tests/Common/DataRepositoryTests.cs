using System;
using Main.Core.Game.Common;
using NUnit.Framework;
using UniRx;

namespace Core.Game.Tests.Common
{
    [TestFixture]
    public class DataRepositoryTests
    {
        //using int for readability
        private TestDataRepository _dataRepository;

        [SetUp]
        public void SetUp()
        {
            _dataRepository = new TestDataRepository();
        }

        [Test]
        public void WhenNotSetWithValues_MostRecentIsThrow()
        {
            var mostRecent = _dataRepository.GetMostRecent();
            Exception returnedValue = null;

            mostRecent
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(
                    _ =>
                    {
                        //do nothing
                    },
                    e => { returnedValue = e; }
                );

            Assert.That(returnedValue, Is.TypeOf<InvalidOperationException>().And.Not.Null);
        }

        [Test]
        public void WhenNotSetWithValues_ObservableStreamIsNotInvokedAtAll()
        {
            var observable = _dataRepository.GetObservableStream();
            var isInvoked = false;
            
            observable
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(
                    _ => { isInvoked = true;}
                );

            Assert.That(isInvoked, Is.False);
        }

        [Test]
        public void WhenSetWithValue_MostRecentIsThatValue()
        {
            const int pushedValue = 1;
            _dataRepository.Set(pushedValue);
            var mostRecent = _dataRepository.GetMostRecent();
            int? returnedValue = null;
            
            mostRecent
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(
                    actualValue => { returnedValue = actualValue; }
                );

            Assert.That(returnedValue, Is.EqualTo(pushedValue));
        }
        
        [Test]
        public void WhenSetWithValue_ObservableStreamIsPushedThatValue()
        {
            const int pushedValue = 1;
            _dataRepository.Set(pushedValue);
            var observableStream = _dataRepository.GetObservableStream();
            int? returnedValue = null;
            
            observableStream
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(
                    actualValue => { returnedValue = actualValue; }
                );

            Assert.That(returnedValue, Is.EqualTo(pushedValue));
        }

        private class TestDataRepository : DataRepository<int?>
        {
        }
    }
}