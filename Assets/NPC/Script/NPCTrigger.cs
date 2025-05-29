using System;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] NPC_Dialogue npcDialogue;
    [SerializeField] Sprite npcSprite;
    [SerializeField] string npcName;
    [SerializeField] string[] npcDialogueLines;
    [SerializeField] Canvas npcCanvas;


    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D  other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger");
            npcCanvas.gameObject.SetActive(true);
            npcDialogue.gameObject.SetActive(true);
            string[] lines = new string[] { "Hello", "How r u", "I'm not fine" };
            npcDialogue.SetUpDialogue(lines, npcSprite, npcName);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        npcCanvas.gameObject.SetActive(false);
        npcDialogue.gameObject.SetActive(false);
    }
}

