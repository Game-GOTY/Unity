using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : Main
{
    [SerializeField] protected Movement playerMovement;
    [SerializeField] private Dialogue dialogue; // Reference to Dialogue component
    private Vector3 dialogueBaseScale; // Store Dialogue's original scale

    private float timer = 3;
    protected override void Start()
    {
        base.Start();
        if (dialogue != null)
        {
            dialogueBaseScale = dialogue.transform.localScale; // Store original scale
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPlayerMovement();
    }
    protected virtual void LoadPlayerMovement()
    {
        if (playerMovement != null) return;
        playerMovement = GetComponentInChildren<Movement>();
    }

    private void Update()
    {

        if (playerMovement.CurrentState() == PlayerState.Idling)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                selfTalk("I should get moving... Otherwise, Founder gonna kick my a**");
            }
        }else{
            selfTalk("", 0);
        }
    }

    private void LateUpdate()
    {
        // Counter-scale Dialogue to prevent flipping
        if (dialogue != null)
        {
            Vector3 parentScale = transform.localScale;
            dialogue.transform.localScale = new Vector3(
                dialogueBaseScale.x / parentScale.x,
                dialogueBaseScale.y / parentScale.y,
                dialogueBaseScale.z / parentScale.z
            );
        }
    }

    private void selfTalk(string text, float duration=-1){
        if (dialogue == null)
            return;
        if (duration == 0){
            dialogue.ShowDialogue(text,0);
        }else{
            dialogue.ShowDialogue(text);
        }
        timer = 3;
    }
}
