using UnityEngine;
using HauntedHospital.Core;
using HauntedHospital.Dialogue;

namespace HauntedHospital.Dialogue.Interactions
{
    public class StateAwareDialogueTrigger : MonoBehaviour
    {
        [Header("Dialogue Variants")]
        [SerializeField] private DialogueData dayDialogue;
        [SerializeField] private DialogueData nightDialogue;

        public void TriggerDialogue()
        {
            if (DialogueManager.Instance == null) return;

            GameState currentState = GameManager.Instance != null ? GameManager.Instance.GetCurrentState() : GameState.Day;
            
            DialogueData toTrigger = (currentState == GameState.Night) ? nightDialogue : dayDialogue;

            if (toTrigger != null)
            {
                DialogueManager.Instance.StartDialogue(toTrigger);
            }
            else
            {
                Debug.LogWarning($"[StateAwareDialogueTrigger] Missing dialogue for state {currentState} on {gameObject.name}");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerDialogue();
            }
        }
    }
}
