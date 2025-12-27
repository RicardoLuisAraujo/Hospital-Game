using UnityEngine;
using System.Collections.Generic;

namespace HauntedHospital.Dialogue
{
    [System.Serializable]
    public struct DialogueChoice
    {
        public string choiceText;
        public DialogueData nextDialogue;
    }

    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Haunted Hospital/Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        public string speakerName;
        [TextArea(3, 10)]
        public string[] dialogueLines;
        
        public List<DialogueChoice> choices;
    }
}
