using UnityEngine;
using UnityEngine.Rendering;
using HauntedHospital.Core;

namespace HauntedHospital.Environment
{
    public class EnvironmentMoodManager : MonoBehaviour
    {
        [System.Serializable]
        public struct MoodSettings
        {
            public Color ambientColor;
            public float ambientIntensity;
            public Color lightColor;
            public float lightIntensity;
        }

        [Header("Moods")]
        [SerializeField] private MoodSettings dayMood;
        [SerializeField] private MoodSettings nightMood;
        
        [Header("References")]
        [SerializeField] private Light mainDirectionalLight;
        [SerializeField] private float transitionSpeed = 2.0f;

        private MoodSettings targetMood;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void Start()
        {
            // Set initial mood
            if (GameManager.Instance != null)
                HandleGameStateChanged(GameManager.Instance.GetCurrentState());
        }

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

            // Smoothly transition light properties
            mainDirectionalLight.color = Color.Lerp(mainDirectionalLight.color, targetMood.lightColor, Time.deltaTime * transitionSpeed);
            mainDirectionalLight.intensity = Mathf.Lerp(mainDirectionalLight.intensity, targetMood.lightIntensity, Time.deltaTime * transitionSpeed);

            // Smoothly transition ambient lighting (requires manual setup in Lighting Window to use Color mode)
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetMood.ambientColor, Time.deltaTime * transitionSpeed);
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, targetMood.ambientIntensity, Time.deltaTime * transitionSpeed);
        }
    }
}
