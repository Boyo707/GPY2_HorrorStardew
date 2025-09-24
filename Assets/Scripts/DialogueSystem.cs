using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea(2, 5)] public string text;
}

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    [Header("UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private float typingSpeed = 0.02f;

    private Queue<DialogueLine> sentences;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private DialogueLine currentLine;

    public bool IsDialogueActive { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        sentences = new Queue<DialogueLine>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(List<DialogueLine> dialogueLines)
    {
        dialoguePanel.SetActive(true);

        TestPlayerController pc = Object.FindFirstObjectByType<TestPlayerController>();
        if (pc != null) pc.enabled = false;

        Rigidbody rb = pc != null ? pc.GetComponent<Rigidbody>() : null;
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }

        sentences.Clear();
        foreach (DialogueLine line in dialogueLines)
            sentences.Enqueue(line);

        IsDialogueActive = true;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentLine.text;
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = sentences.Dequeue();
        speakerNameText.text = currentLine.speakerName;
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        speakerNameText.text = "";

        IsDialogueActive = false;

        TestPlayerController pc = Object.FindFirstObjectByType<TestPlayerController>();
        if (pc != null) pc.enabled = true;
    }
}
