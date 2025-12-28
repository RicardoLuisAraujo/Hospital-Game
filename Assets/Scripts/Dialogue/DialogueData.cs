using UnityEngine;
using System.Collections.Generic;

namespace HauntedHospital.Dialogue
{
    /// <summary>
    /// Represents a single choice a player can make during a conversation.
    /// </summary>
    [System.Serializable]
    public struct DialogueChoice
    {
        [Tooltip("The text that will appear on the button.")]
        public string choiceText;
        
        [Tooltip("The next dialogue asset to load if this choice is picked. Leave empty to end the conversation.")]
        public DialogueData nextDialogue;
    }

    /// <summary>
    /// A ScriptableObject that stores the actual text content of a conversation.
    /// You can create these in the Project window by right-clicking.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Haunted Hospital/Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [Header("Dialogue Content")]
        public string speakerName;
        
        [TextArea(3, 10)] // Makes the text box larger in the Inspector
        public string[] dialogueLines;
        
        [Header("Branching")]
        public List<DialogueChoice> choices;
    }
}
