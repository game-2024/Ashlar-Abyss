using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Cinemachine;


//Script is mainly for identifiying a Player object
public class PlayerStatsScript : MonoBehaviour//, IDamageable
{
    [SerializeField] private PlayerHealthManagerScript healthManagerScript;
    [SerializeField] private TimeIntervalComponentScript timer;
    [SerializeField] private TimeIntervalComponentScript hitCooldownTimer;


    private Rigidbody playerRB;

    private bool isHit = false;

    public float speed;


    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }


    public void Move(Vector3 movementVector)
    {
        playerRB.velocity = movementVector * speed;
    }

    public float GetPlayerSpeed()
    {
        return speed;
    }

    public void TakeDamage(float damage_to_take)
    {

        healthManagerScript.DamagePlayer(damage_to_take);

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



