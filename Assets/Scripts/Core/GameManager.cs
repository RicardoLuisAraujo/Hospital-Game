using UnityEngine;

namespace HauntedHospital.Core
{
    /// <summary>
    /// The GameState enum defines the different modes the game can be in.
    /// This helps other scripts know if the player should be moving, 
    /// if a dialogue is playing, or if it's Day vs Night.
    /// </summary>
    public enum GameState
    {
        Day,
        Night,
        Dialogue,
        Paused
    }

    /// <summary>
    /// The GameManager is the "brain" of the game. 
    /// It uses the Singleton pattern (Instance) so it can be accessed from anywhere.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // Singleton Instance: Allows other scripts to call GameManager.Instance.SetGameState(...)
        public static GameManager Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private GameState currentGameState = GameState.Day;

        // C# Events: Other scripts (like the lighting manager) "subscribe" to this
        // so they are automatically notified when the state changes.
        public delegate void GameStateChanged(GameState newState);
        public static event GameStateChanged OnGameStateChanged;

        private void Awake()
        {
            // Standard Singleton setup: Ensure only one GameManager exists in the scene
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep this alive when changing scenes
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Returns the current state of the game
        public GameState GetCurrentState() => currentGameState;

        /// <summary>
        /// Updates the game state and notifies all subscribed listeners.
        /// </summary>
        public void SetGameState(GameState newState)
        {
            if (currentGameState == newState) return;

            currentGameState = newState;
            
            // Trigger the event to let other systems know they need to update
            OnGameStateChanged?.Invoke(currentGameState);
            
            Debug.Log($"[GameManager] Game State changed to: {currentGameState}");
        }

        /// <summary>
        /// Simple helper to switch between Day and Night for testing.
        /// </summary>
        public void ToggleDayNight()
        {
            if (currentGameState == GameState.Day)
                SetGameState(GameState.Night);
            else if (currentGameState == GameState.Night)
                SetGameState(GameState.Day);
        }
    }
}
