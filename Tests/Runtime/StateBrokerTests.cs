using System;
using NUnit.Framework;

namespace GF.Library.StateBroker.Tests
{
    [TestFixture]
    public class StateBrokerTests
    {
        private StateBroker _stateBroker;
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
            _stateBroker = new StateBroker();

            var stars = new ObservableStateProperty<int>(10);
            var coins = new ObservableStateProperty<int>(10);
            
            _stateBroker.AddProperty(stars);
            _stateBroker.AddProperty(coins);
            
            _state = new State(stars, coins);
        }

        [Test]
        public void WhenAddSamePropertyTwice_ThrowsException()
        {
            _stateBroker = new StateBroker();
            var stars = new ObservableStateProperty<int>(10);
            
            _stateBroker.AddProperty(stars);

            Assert.Throws<Exception>(() =>
            {
                _stateBroker.AddProperty(stars);
            });
        }
        
        [Test]
        public void WhenRemovePropertyThatDoesNotExist_ThrowsException()
        {
            _stateBroker = new StateBroker();
            var stars = new ObservableStateProperty<int>(10);
            
            Assert.Throws<Exception>(() =>
            {
                _stateBroker.RemoveProperty(stars);
            });
        }

        [Test]
        public void WhenTransacting_NotifiesObserversOnCommit()
        {
            var stateObserverCalled = false;
            
            void ObserverCallback()
            {
                stateObserverCalled = true;
            }
            
            _state.Stars.Subscribe(ObserverCallback);
            
            _stateBroker.StartTransaction();
            
            _state.Stars.Value++;
            
            Assert.That(!stateObserverCalled);
            
            _stateBroker.Commit();
            
            Assert.That(stateObserverCalled);
        }

        [Test]
        public void OnStartTransaction_IfAlreadyTransacting_ThrowsException()
        {
            _stateBroker.StartTransaction();
            Assert.Throws<TransactionException>(() =>
            {
                _stateBroker.StartTransaction();
            });
        }

        [Test]
        public void OnTransactionFinish_NotifiesObservers()
        {
            var stateObserverCalled = false;
            
            void ObserverCallback()
            {
                stateObserverCalled = true;
            }
            
            _state.Stars.Subscribe(ObserverCallback);
            
            _stateBroker.StartTransaction();
            _state.Stars.Value++;
            _stateBroker.Commit();
            
            Assert.That(stateObserverCalled);
        }
        
        
        [Test]
        public void OnTransactionFinish_WithObserverWatchingMultiple_NotifiesObserverOneTime()
        {
            var stateObserverCalledCount = 0;
            
            void ObserverCallback()
            {
                stateObserverCalledCount++;
            }
            
            _state.Stars.Subscribe(ObserverCallback);
            _state.Coins.Subscribe(ObserverCallback);
            
            _stateBroker.StartTransaction();
            _state.Stars.Value++;
            _state.Coins.Value++;
            _stateBroker.Commit();
            
            Assert.That(stateObserverCalledCount == 1);
        }
    }
}