using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Script is mainly for identifiying a Player object
public class PlayerStatsScript : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerHealthManagerScript healthManagerScript;

    public float Speed;

    public float GetPlayerSpeed()
    {
        return Speed;
    }

    public void TakeDamage(int damage_to_take)
    {
        ///healthManagerScript.DamagePlayer();
    }
}



