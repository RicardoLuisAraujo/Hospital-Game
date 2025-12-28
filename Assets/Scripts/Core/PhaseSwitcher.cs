using UnityEngine;
using UnityEngine.InputSystem;
using HauntedHospital.Core;

namespace HauntedHospital.Core.DebugTools
{
    /// <summary>
    /// This is a simple debug tool to allow the designer to "force" 
    /// a change between Day and Night using a key press.
    /// </summary>
    public class PhaseSwitcher : MonoBehaviour
    {
        private void Update()
        {
            // Keyboard.current is part of the "New Input System".
            // It allows us to check for key presses without complex setup.
            if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
            {
                // We talk to the GameManager to trigger the transition
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ToggleDayNight();
                }
            }
        }
    }
}
