using UnityEngine;
using HauntedHospital.Core;
using System.Collections.Generic;

namespace HauntedHospital.Dialogue
{
    /// <summary>
    /// The DialogueManager handles the sequence of dialogue lines and choices.
    /// It communicates with the UI (via events) and the GameManager (to freeze the player).
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        private DialogueData currentDialogue;
        private int currentLineIndex = 0;

        // C# Events for UI to subscribe to:
        // This allows the UI script (which you'll build later) to react automatically
        // when dialogue starts, changes, or ends.
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

        /// <summary>
        /// Starts a new conversation using the provided DialogueData asset.
        /// </summary>
        public void StartDialogue(DialogueData data)
        {
            if (data == null) return;
            
            currentDialogue = data;
            currentLineIndex = 0;
            
            // Freeze the player by changing the game state
            if (GameManager.Instance != null)
                GameManager.Instance.SetGameState(GameState.Dialogue);

            OnDialogueStarted?.Invoke(data);
            DisplayNextLine();
        }

        /// <summary>
        /// Logic to move to the next sentence or show player choices.
        /// </summary>
        public void DisplayNextLine()
        {
            if (currentLineIndex < currentDialogue.dialogueLines.Length)
            {
                // Send the current line to the UI
                OnLineUpdated?.Invoke(currentDialogue.speakerName, currentDialogue.dialogueLines[currentLineIndex]);
                currentLineIndex++;
            }
            else
            {
                // If we've run out of lines, check if there are branching choices
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

        /// <summary>
        /// Called when the player clicks a choice button.
        /// </summary>
        public void SelectChoice(int choiceIndex)
        {
            if (choiceIndex < 0 || choiceIndex >= currentDialogue.choices.Count) return;

            DialogueData next = currentDialogue.choices[choiceIndex].nextDialogue;
            
            // If the choice leads to more dialogue, start it
            if (next != null)
            {
                StartDialogue(next);
            }
            else
            {
                EndDialogue();
            }
        }

        /// <summary>
        /// Closes the dialogue mode and returns control to the player.
        /// </summary>
        private void EndDialogue()
        {
            currentDialogue = null;
            
            if (GameManager.Instance != null)
                GameManager.Instance.SetGameState(GameState.Day); // Return to default exploration

            OnDialogueEnded?.Invoke();
        }
    }
}
