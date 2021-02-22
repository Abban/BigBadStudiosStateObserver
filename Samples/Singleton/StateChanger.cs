using UnityEngine;

namespace StateBrokerSingleton
{
    public class StateChanger : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetButtonDown("Fire1") && !Input.GetButtonDown("Fire2")) return;
            
            StateManager.StartTransaction();

            if (Input.GetButtonDown("Fire1"))
            {
                StateManager.AddStars();
            }
            
            if (Input.GetButtonDown("Fire2"))
            {
                StateManager.AddCoins();
            }
            
            StateManager.Commit();
        }
    }
}