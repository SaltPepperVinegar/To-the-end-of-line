using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{   
    public GameObject InventoryMenu;
    private bool menuActivated;

    public ItemSlot[] itemSlot;
    public ItemSO[] itemSOs;

    public GameObject[] itemInstances;
    // Start is called before the first frame update
    void Start()
    {
        InventoryMenu.SetActive(false);
        menuActivated = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale =1;  
            InventoryMenu.SetActive(false);
            menuActivated = false;

        } else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;


        }

    }

    public bool UseItem(string itemName)
    {
        for(int i =0; i<itemSOs.Length;i++)
        {
            if(itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }
    public int AddItem(string itemName, int quantity, Sprite sprite, String itemDescription)
    {
        for (int i =0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false || itemSlot[i].name == name || itemSlot[i].quantity == 0 )
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, sprite, itemDescription);
                if (leftOverItems >0){
                    leftOverItems = AddItem(itemName, leftOverItems, sprite, itemDescription);
                }
                return leftOverItems;

            } 

        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0 ; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;    
        }
    }

    public void instantiateItem(string itemName, int quantity,Vector3 position)
    {
        foreach(GameObject gameObject in itemInstances)
        {
            if(gameObject.GetComponent<Item>().itemName == itemName){
                GameObject newItemInstance = Instantiate(gameObject);
                newItemInstance.transform.position = position;
                newItemInstance.GetComponent<Item>().quantity = quantity; 
                newItemInstance.SetActive(true);
            }
        }

    }
}
