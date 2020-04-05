using UnityEngine;

namespace StateBrokerSingleton
{
    public class Observer : MonoBehaviour
    {
        private void OnEnable()
        {
            StateManager.Stars.Action += OnStateChanged;
            StateManager.Coins.Action += OnStateChanged;
        }


        private void OnDisable()
        {
            StateManager.Stars.Action -= OnStateChanged;
            StateManager.Coins.Action -= OnStateChanged;
        }


        private static void OnStateChanged()
        {
            Debug.Log($"Stars: {StateManager.Stars.Value} | Coins: {StateManager.Coins.Value}");
        }
    }
}