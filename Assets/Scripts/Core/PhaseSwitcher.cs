using UnityEngine;
using UnityEngine.InputSystem;
using HauntedHospital.Core;

namespace HauntedHospital.Core.DebugTools
{
    public class PhaseSwitcher : MonoBehaviour
    {
        private void Update()
        {
            // Use the New Input System instead of legacy Input.GetKeyDown
            if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.ToggleDayNight();
                }
            }
        }
    }
}
