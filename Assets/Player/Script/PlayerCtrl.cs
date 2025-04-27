using UnityEngine;

public class PlayerCtrl : Main
{
    [SerializeField] protected Movement playerMovement;

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
}
