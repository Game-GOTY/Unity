using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Dialogue : MonoBehaviour
{
    [SerializeField] Image imageNPC;
    [SerializeField] Text nameNPC;
    [SerializeField] Text dialogueText;
    Coroutine currentTypingCoroutine;


    [Header("Test NPC Dialogue")]
    [SerializeField] Sprite spriteNPC;
    [SerializeField] string name_NPC;
    // [SerializeField] string[] dialogue; []
    

    private void OnEnable()
    {
        // Test
        // SetUpDialogue(dialogue, spriteNPC, name_NPC);
    }

    void ChangeImageNPC(Sprite spriteNPC)
    {
        imageNPC.sprite = spriteNPC;
    }

    void ChangeNameNPC(string name)
    {
        nameNPC.text = name;
    }

    void ChangeDialogue(string dialogue)
    {
        if (currentTypingCoroutine != null)
        {
            StopCoroutine(currentTypingCoroutine);
        }
        dialogueText.text = "";
        currentTypingCoroutine = StartCoroutine(TypeDialogue(dialogue));
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            yield return new WaitForSeconds(0.07f);
            dialogueText.text += letter;
            if (Input.GetMouseButton(0))
            {
                break;
            }
        }
    }

    private IEnumerator RunDialogue(string[] dialogues)
    {
        foreach (string line in dialogues)
        {
            ChangeDialogue(line);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0)); 
        }

        gameObject.SetActive(false);
    }

    public void SetUpDialogue(string[] dialogues, Sprite spriteNPC, string name)
    {
        ChangeImageNPC(spriteNPC);
        ChangeNameNPC(name);
        StartCoroutine(RunDialogue(dialogues));
    }
}
