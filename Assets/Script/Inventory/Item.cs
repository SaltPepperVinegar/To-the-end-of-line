using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public int quantity;
    [SerializeField] public Sprite sprite;
    private InventoryManager inventoryManager;

    [TextArea]
    [SerializeField] public string itemDescription;


    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();    
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName,quantity,sprite, itemDescription);
            if ( leftOverItems <= 0){
                Destroy(gameObject);
            } else {
                quantity = leftOverItems;
            }
        }
    }
}
