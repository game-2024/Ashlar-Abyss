using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Script is mainly for identifiying a Player object
public class PlayerStatsScript : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerHealthManagerScript healthManagerScript;
    [SerializeField] private TimeIntervalComponentScript timer;
    [SerializeField] private TimeIntervalComponentScript hitCooldownTimer;

    private bool isHit = false;

    public float Speed;

    public float GetPlayerSpeed()
    {
        return Speed;
    }

    public void TakeDamage(int damage_to_take)
    {
        healthManagerScript.DamagePlayer();

        hitCooldownTimer.ResetTimer();
        hitCooldownTimer.ResumeTimer();

        isHit = true;

        if (!IfHealthLowerThanHalve())
        {
            timer.PauseTimer();
            timer.ResetTimer();
        }
    }

    public void HitCooldownReached()
    {
        isHit = false;
        hitCooldownTimer.PauseTimer();
        hitCooldownTimer.ResetTimer();
        RegenHealIfLowerThanHalf();
    }

    public void RegenHealIfLowerThanHalf()
    {
        if (IfHealthLowerThanHalve() && isHit == false )
        {
            healthManagerScript.HealPlayer();
            timer.ResumeTimer();
        }
        else
        {
            timer.PauseTimer();
            timer.ResetTimer();
        }
    }

    private bool IfHealthLowerThanHalve()
    {
        return healthManagerScript.CurrentHealth < healthManagerScript.MaxHealth / 2;
    }


}



