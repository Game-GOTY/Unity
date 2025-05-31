using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class DialogueInfo
{
    public Sprite _spriteNPC;
    public string _nameNPC;
    public string[] _dialogue;

    public DialogueInfo(Sprite spriteNPC, string nameNPC, string[] dialogue)
    {
        _spriteNPC = spriteNPC;
        _nameNPC = nameNPC;
        _dialogue = dialogue;
    }
}
public class NPC_Dialogue : MonoBehaviour
{
    [SerializeField] Image imageNPC;
    [SerializeField] Text nameNPC;
    [SerializeField] Text dialogueText;
    Coroutine currentTypingCoroutine;

    public DialogueInfo[] dialogues;
    public int index = 0;
    //private void OnEnable()
    //{
    //    //Test
    //    SetUpDialogue(dialogues[index]._dialogue, dialogues[index]._spriteNPC, dialogues[index]._nameNPC);
    //}
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

    private IEnumerator RunDialogue(string[] dialogue)
    {
        foreach (string line in dialogue)
        {
            ChangeDialogue(line);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0)); 
        }
        index++;
        if (index < dialogues.Length)
        {
            SetUpDialogue(dialogues[index]._dialogue, dialogues[index]._spriteNPC, dialogues[index]._nameNPC);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetUpDialogue(string[] dialogues, Sprite spriteNPC, string name)
    {
        ChangeImageNPC(spriteNPC);
        ChangeNameNPC(name);
        StartCoroutine(RunDialogue(dialogues));
    }
}
