using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{    
    [field: SerializeField] public float MaxHealth { get; set;} = 5f;
    
    public float CurrentHealth { get; set;}

    [SerializeField] HealthBar healthbar;

    private void Awake(){
        healthbar = GetComponentInChildren<HealthBar>();
        CurrentHealth = MaxHealth;

    }
    void Start()
    {
        healthbar.UpdateHealthBar(CurrentHealth,MaxHealth);

    }

    public void Damage(float dmg) {
        Debug.Log("take damage " +dmg);
        CurrentHealth-=dmg;
        healthbar.UpdateHealthBar(CurrentHealth,MaxHealth);
        if (CurrentHealth<=0) {
            Die();
        } else{
            gameObject.GetComponent<Enemy>().enraged();

        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
