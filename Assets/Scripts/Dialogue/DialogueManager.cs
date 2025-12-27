using UnityEngine;
using HauntedHospital.Core;
using System.Collections.Generic;

namespace HauntedHospital.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        private DialogueData currentDialogue;
        private int currentLineIndex = 0;

        // Events for UI to subscribe to
        public delegate void DialogueStarted(DialogueData data);
        public static event DialogueStarted OnDialogueStarted;

        public delegate void LineUpdated(string speaker, string content);
        public static event LineUpdated OnLineUpdated;

        public delegate void ChoicesAvailable(List<DialogueChoice> choices);
        public static event ChoicesAvailable OnChoicesAvailable;

        public delegate void DialogueEnded();
        public static event DialogueEnded OnDialogueEnded;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void StartDialogue(DialogueData data)
        {
            currentDialogue = data;
            currentLineIndex = 0;
            
            if (GameManager.Instance != null)
                GameManager.Instance.SetGameState(GameState.Dialogue);

            OnDialogueStarted?.Invoke(data);
            DisplayNextLine();
        }

        public void DisplayNextLine()
        {
            if (currentLineIndex < currentDialogue.dialogueLines.Length)
            {
                OnLineUpdated?.Invoke(currentDialogue.speakerName, currentDialogue.dialogueLines[currentLineIndex]);
                currentLineIndex++;
            }
            else
            {
                // If there are choices, show them. Otherwise, end dialogue.
                if (currentDialogue.choices != null && currentDialogue.choices.Count > 0)
                {
                    OnChoicesAvailable?.Invoke(currentDialogue.choices);
                }
                else
                {
                    EndDialogue();
                }
            }
        }

        public void SelectChoice(int choiceIndex)
        {
            if (choiceIndex < 0 || choiceIndex >= currentDialogue.choices.Count) return;

            DialogueData next = currentDialogue.choices[choiceIndex].nextDialogue;
            if (next != null)
            {
                StartDialogue(next);
            }
            else
            {
                EndDialogue();
            }
        }

        private void EndDialogue()
        {
            currentDialogue = null;
            
            if (GameManager.Instance != null)
                GameManager.Instance.SetGameState(GameState.Day); // Return to default exploration

            OnDialogueEnded?.Invoke();
        }
    }
}
