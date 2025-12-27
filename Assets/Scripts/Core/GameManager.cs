using UnityEngine;

namespace HauntedHospital.Core
{
    public enum GameState
    {
        Day,
        Night,
        Dialogue,
        Paused
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private GameState currentGameState = GameState.Day;

        // Events for other systems to listen to
        public delegate void GameStateChanged(GameState newState);
        public static event GameStateChanged OnGameStateChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public GameState GetCurrentState() => currentGameState;

        public void SetGameState(GameState newState)
        {
            if (currentGameState == newState) return;

            currentGameState = newState;
            OnGameStateChanged?.Invoke(currentGameState);
            
            Debug.Log($"[GameManager] Game State changed to: {currentGameState}");
        }

        // Toggle helper for testing
        public void ToggleDayNight()
        {
            if (currentGameState == GameState.Day)
                SetGameState(GameState.Night);
            else if (currentGameState == GameState.Night)
                SetGameState(GameState.Day);
        }
    }
}
