using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to Dialogue component
    [SerializeField] private string[] dialogueLines = { "Hello, traveler!", "Need help?" };
    private int currentDialogueIndex = 0;

    private void Awake()
    {
        // Find Dialogue component in children if not assigned
        if (dialogue == null)
        {
            dialogue = GetComponentInChildren<Dialogue>();
            if (dialogue == null)
            {
                Debug.LogWarning("No Dialogue component found for NPC: " + gameObject.name);
            }
        }
    }

    private void OnMouseDown()
    {
        ShowNextDialogue();
    }

    private void ShowNextDialogue()
    {
        if (dialogue == null || dialogueLines.Length == 0)
            return;

        dialogue.ShowDialogue(dialogueLines[currentDialogueIndex]);
        currentDialogueIndex = (currentDialogueIndex + 1) % dialogueLines.Length;
    }
}