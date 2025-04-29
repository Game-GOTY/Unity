using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Punching : Main
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected BoxCollider2D punchHitbox;
    [SerializeField] protected Movement movement;
    [SerializeField] protected float punchCoolDown = 0.5f;
    [SerializeField] protected bool isPunching = false;
    protected override void Start()
    {
        base.Start();
        punchHitbox.enabled = false;
    }
    protected virtual void Update()
    {
        Punch();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPlayerCtrl();
        LoadMovement();
        LoadPunchHitBox();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        playerCtrl = transform.parent.parent.GetComponent<PlayerCtrl>();
    }
    protected virtual void LoadMovement()
    {
        if (movement != null) return;
        movement = transform.parent.parent.GetComponentInChildren<Movement>();
    }
    protected virtual void LoadPunchHitBox()
    {
        if (punchHitbox != null) return;
        punchHitbox = GetComponentInChildren<BoxCollider2D>();
    }
    protected virtual void Punch()
    {

    }
}
