using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public string dialogueName;                 
    public List<DialogueLine> lines = new();  
}

public class Dialogues : MonoBehaviour
{
    [Header("Dialogues")]
    public List<DialogueData> dialogues = new List<DialogueData>();

    public void TriggerDialogue(string dialogueName)
    {
        DialogueData data = dialogues.Find(d => d.dialogueName == dialogueName);
        if (data != null)
        {
            DialogueSystem.Instance.StartDialogue(data.lines);
        }
        else
        {
            Debug.LogWarning($"Dialogue '{dialogueName}' not found!");
        }
    }
}
