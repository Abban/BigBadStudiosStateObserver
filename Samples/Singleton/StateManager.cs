using System;
using GF.Library.StateObserver;
using UnityEngine;

namespace StateBrokerSingleton
{
    public class StateManager : MonoBehaviour
    {
        public GameState GameState { get; private set; }
        public ObservableStateBroker StateBroker { get; private set; }
        private static StateManager _stateManager;
        
        public static IObservableStateProperty<int> Coins => Instance.GameState.Coins;
        public static IObservableStateProperty<int> Stars => Instance.GameState.Stars;
        
        public static StateManager Instance
        {
            get
            {
                if (_stateManager) return _stateManager;

                _stateManager = FindObjectOfType(typeof(StateManager)) as StateManager;

                if (!_stateManager || _stateManager == null)
                {
                    Debug.LogError("There needs to be one active StateManager script on a GameObject in your scene.");
                }
                else
                {
                    _stateManager.Init();
                }

                return _stateManager;
            }
        }

        private void Init()
        {
            if (StateBroker != null) return;
            
            StateBroker = new ObservableStateBroker();

            var stars = new ObservableStateProperty<int>(StateBroker, 10);
            var coins = new ObservableStateProperty<int>(StateBroker, 10);
                
            GameState = new GameState(stars, coins);
        }


        public static void AddStars()
        {
            Instance.GameState.Stars.Value++;
        }
        
        
        public static void AddCoins()
        {
            Instance.GameState.Coins.Value++;
        }


        public static void NotifyObservers()
        {
            Instance.StateBroker.NotifyObservers();
        }
    }
}