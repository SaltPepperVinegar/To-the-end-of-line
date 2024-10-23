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
        return false;
    }
    public enum StatToChange
    {
        none,
        health,
        stamina
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
