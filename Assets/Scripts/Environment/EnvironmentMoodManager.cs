using UnityEngine;
using UnityEngine.Rendering;
using HauntedHospital.Core;

namespace HauntedHospital.Environment
{
    /// <summary>
    /// This script handles the visual transition between Day and Night.
    /// It listens to the GameManager and smoothly "lerps" (interpolates) 
    /// the lighting colors and intensities.
    /// </summary>
    public class EnvironmentMoodManager : MonoBehaviour
    {
        /// <summary>
        /// A small helper class to group light settings together in the Inspector.
        /// </summary>
        [System.Serializable]
        public struct MoodSettings
        {
            public Color ambientColor;     // Sky/Background light
            public float ambientIntensity;
            public Color lightColor;       // Main sun/moon light
            public float lightIntensity;
        }

        [Header("Mood Configurations")]
        [SerializeField] private MoodSettings dayMood;
        [SerializeField] private MoodSettings nightMood;
        
        [Header("Scene References")]
        [Tooltip("The Directional Light in your scene that acts as the sun/moon.")]
        [SerializeField] private Light mainDirectionalLight;
        
        [Tooltip("How fast the transition happens (in seconds).")]
        [SerializeField] private float transitionSpeed = 2.0f;

        private MoodSettings targetMood;

        private void OnEnable()
        {
            // Subscribe to the event so we know when the phase changes
            GameManager.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            // Always unsubscribe when the object is destroyed to avoid memory leaks
            GameManager.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void Start()
        {
            // Set the initial lighting based on the current game state
            if (GameManager.Instance != null)
                HandleGameStateChanged(GameManager.Instance.GetCurrentState());
        }

        /// <summary>
        /// Called automatically by the GameManager when the state changes.
        /// </summary>
        private void HandleGameStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Day:
                    targetMood = dayMood;
                    break;
                case GameState.Night:
                    targetMood = nightMood;
                    break;
            }
        }

        private void Update()
        {
            if (mainDirectionalLight == null) return;

            // Color.Lerp and Mathf.Lerp smoothly transition values 
            // from the current color to the "target" color over time.

            // 1. Smoothly transition the main light
            mainDirectionalLight.color = Color.Lerp(mainDirectionalLight.color, targetMood.lightColor, Time.deltaTime * transitionSpeed);
            mainDirectionalLight.intensity = Mathf.Lerp(mainDirectionalLight.intensity, targetMood.lightIntensity, Time.deltaTime * transitionSpeed);

            // 2. Smoothly transition the ambient (sky) lighting
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetMood.ambientColor, Time.deltaTime * transitionSpeed);
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, targetMood.ambientIntensity, Time.deltaTime * transitionSpeed);
        }
    }
}
