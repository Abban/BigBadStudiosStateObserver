using System;
using NUnit.Framework;

namespace BBX.Library.StateObserver.Tests
{
    [TestFixture]
    public class StateBrokerTests
    {
        private ObservableStateBroker _stateBroker;
        private State _state;

        private class State
        {
            public IObservableStateProperty<int> Stars { get; }
            public IObservableStateProperty<int> Coins { get; }

            public State(
                IObservableStateProperty<int> stars,
                IObservableStateProperty<int> coins)
            {
                Stars = stars;
                Coins = coins;
            }
        }
        
        [SetUp]
        public void SetupStateBroker()
        {
            _stateBroker = new ObservableStateBroker();

            var stars = new ObservableStateProperty<int>(_stateBroker, 10);
            var coins = new ObservableStateProperty<int>(_stateBroker, 10);
            
            _state = new State(stars, coins);
        }

        [Test]
        public void OnTransactionFinish_NotifiesObservers()
        {
            var notifier = _stateBroker as IStateObserverNotifier;
            var stateObserverCalled = false;
            
            void ObserverCallback()
            {
                stateObserverCalled = true;
            }
            
            _state.Stars.Action += ObserverCallback;
            _state.Stars.Value++;

            notifier.NotifyObservers();
            
            Assert.That(stateObserverCalled);
        }
        
        
        [Test]
        public void OnTransactionFinish_WithObserverWatchingMultiple_NotifiesObserverOneTime()
        {
            var notifier = _stateBroker as IStateObserverNotifier;
            var stateObserverCalledCount = 0;
            
            void ObserverCallback()
            {
                stateObserverCalledCount++;
            }
            
            _state.Stars.Action += ObserverCallback;
            _state.Coins.Action += ObserverCallback;
            
            _state.Stars.Value++;
            _state.Coins.Value++;

            notifier.NotifyObservers();
            
            Assert.That(stateObserverCalledCount == 1);
        }
    }
}