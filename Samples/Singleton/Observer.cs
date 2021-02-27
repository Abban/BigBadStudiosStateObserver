using UnityEngine;

namespace StateBrokerSingleton
{
    public class Observer : MonoBehaviour
    {
        private void OnEnable()
        {
            StateManager.Stars.Subscribe(OnStateChanged);
            StateManager.Coins.Subscribe(OnStateChanged);
        }


        private void OnDisable()
        {
            StateManager.Stars.Unsubscribe(OnStateChanged);
            StateManager.Coins.Unsubscribe(OnStateChanged);
        }


        private static void OnStateChanged()
        {
            Debug.Log($"Stars: {StateManager.Stars.Value} | Coins: {StateManager.Coins.Value}");
        }
    }
}