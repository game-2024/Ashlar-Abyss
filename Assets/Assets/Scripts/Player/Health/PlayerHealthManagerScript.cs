using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayerHealthManager", menuName = "ScriptableObjects/PlayerHealthManagerScriptableObject")]
public class PlayerHealthManagerScript : ScriptableObject
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currHealth;

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public float CurrentHealth
    {
        get { return currHealth; }

        set
        {
            currHealth = value;

            if(currHealth > maxHealth)
            {
                currHealth = maxHealth;
            }

            if(currHealth < 0)
            {
                currHealth = 0;
                OnPlayerDied?.Invoke();
            }

        }
    }

    public int heal = 1;//remove after testing


    [HideInInspector]
    public UnityEvent<float, float> onHealthAdjust;
    public event UnityAction OnPlayerDied;

    private void OnEnable()
    {
        InitializeHealthManager();
    }

    public void InitializeHealthManager()
    {
        currHealth = maxHealth;
    }




    public void HealPlayer()//Healing system for player
    {
        CurrentHealth += heal;

        onHealthAdjust?.Invoke(CurrentHealth, MaxHealth);
        //Revisit: consume health item
    }

  
    public void DamagePlayer(float damage_to_take)//Damage system for player
    {
        CurrentHealth -= damage_to_take;
        onHealthAdjust?.Invoke(CurrentHealth, MaxHealth);

        //Revisit: add invincibility frames
        //Revisit: run player death function

    }
    
}
