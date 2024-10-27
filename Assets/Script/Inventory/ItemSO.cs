using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;

    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if(statToChange == StatToChange.health)
        {   
            PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            if(playerHealth.CurrentHealth == playerHealth.MaxHealth)
            {
                return false;
            } else{
                playerHealth.Damage(-amountToChangeStat);
                return true;
            }
        } 
        if (statToChange == StatToChange.stealth){
            PlayerStealth playerStealth = GameObject.Find("Player").GetComponent<PlayerStealth>();
            if(!playerStealth.stealth){
                playerStealth.setStealth(amountToChangeStat);
                return true;
            } else{
                return false;
            }
        }
        
        return false;
    }
    public enum StatToChange
    {
        none,
        health,
        stamina,
        stealth,
    };
    public enum AttributesToChange
    {
        none,
        strength,
        defense,
        intelligence,
        agility
    }
}
