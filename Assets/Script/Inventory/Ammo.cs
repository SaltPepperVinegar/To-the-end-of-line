using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo: Item
{
    public int Ammotype;

    // Update is called once per frame
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName,quantity,sprite, itemDescription, maxNumberItems);
            collision.gameObject.GetComponent<WeaponControl>().collectAmmo(quantity-leftOverItems,Ammotype);
            if ( leftOverItems <= 0){
                Destroy(gameObject);
            } else {
                quantity = leftOverItems;
            }
        }
    }
}
