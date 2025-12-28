using UnityEngine;
using HauntedHospital.Core;
using HauntedHospital.Dialogue;

namespace HauntedHospital.Dialogue.Interactions
{
    /// <summary>
    /// This advanced trigger allows an NPC to say different things 
    /// depending on whether it is Day or Night in the hospital.
    /// </summary>
    public class StateAwareDialogueTrigger : MonoBehaviour
    {
        [Header("Dialogue Variants")]
        [Tooltip("Conversation used during the Day phase.")]
        [SerializeField] private DialogueData dayDialogue;
        
        [Tooltip("Conversation used during the Night phase.")]
        [SerializeField] private DialogueData nightDialogue;

        /// <summary>
        /// Logic to decide which dialogue asset to send to the DialogueManager.
        /// </summary>
        public void TriggerDialogue()
        {
            if (DialogueManager.Instance == null) return;

            // 1. Check the current state from the GameManager
            GameState currentState = GameManager.Instance != null ? GameManager.Instance.GetCurrentState() : GameState.Day;
            
            // 2. Pick the appropriate dialogue asset
            DialogueData toTrigger = (currentState == GameState.Night) ? nightDialogue : dayDialogue;

            // 3. Start the dialogue
            if (toTrigger != null)
            {
                DialogueManager.Instance.StartDialogue(toTrigger);
            }
            else
            {
                Debug.LogWarning($"[StateAwareDialogueTrigger] Missing dialogue for state {currentState} on {gameObject.name}");
            }
        }

        /// <summary>
        /// Automatically triggers when the player walks into this object's Trigger Collider.
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerDialogue();
            }
        }
    }
}
