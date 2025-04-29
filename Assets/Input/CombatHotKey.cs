using UnityEngine;

public class CombatHotKey : Singleton<CombatHotKey>
{
    protected float maxDashingCd = 5f;
    protected float currentDashingCd = 5f;
    protected float maxPunchingCd = 5f;
    protected float currentPunchingCd = 5f;
    protected bool isDashing = false;
    protected bool isPunching = false;
    protected virtual void Update()
    {
        CheckDashing();
    }
    protected virtual void CheckAttack()
    {
        if ((currentPunchingCd < maxPunchingCd || isPunching) && currentPunchingCd > 0)
        {
            currentPunchingCd -= Time.deltaTime;
            isPunching = false;
        }
        if (currentPunchingCd == maxPunchingCd)
        {
            if (Input.GetKeyUp(KeyCode.J)) isPunching = true;
        }
        if (currentPunchingCd <= 0) currentPunchingCd = maxPunchingCd;
    }
    protected virtual void CheckDashing()
    {
        if ((currentDashingCd < maxDashingCd || isDashing) && currentDashingCd > 0)
        {
            currentDashingCd -= Time.deltaTime;
            isDashing = false;
        }
        if (currentDashingCd == maxDashingCd)
        {
            if (Input.GetKeyUp(KeyCode.E)) isDashing = true;
        }
        if (currentDashingCd <= 0) currentDashingCd = maxDashingCd;
    }
    public virtual bool IsDashing()
    {
        return isDashing;
    }
    public virtual bool IsPunching()
    {
        return isPunching;
    }
}
