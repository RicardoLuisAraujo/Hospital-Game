using UnityEngine;
using HauntedHospital.Dialogue;

namespace HauntedHospital.Dialogue.Interactions
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueData dialogueData;
        [SerializeField] private bool triggerOnStart = false;

        private void Start()
        {
            if (triggerOnStart)
            {
                TriggerDialogue();
            }
        }

        public void TriggerDialogue()
        {
            if (dialogueData != null && DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(dialogueData);
            }
            else
            {
                Debug.LogWarning($"[DialogueTrigger] Missing DialogueData or DialogueManager for {gameObject.name}");
            }
        }

        // Optional: Simple proximity trigger for testing
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerDialogue();
            }
        }
    }
}
