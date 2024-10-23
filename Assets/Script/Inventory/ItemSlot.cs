using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //---------ITEM DATA----------//
    public string itemName = "";
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription =  "";
    //----
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private int maxNumberItems;
    [SerializeField] private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    //--- Item Description slot 
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;
    public Sprite emptySprite;
    
    public void Start()
    {   
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public int AddItem(string itemName, int quantity, Sprite sprite, String itemDescription)
    {   //Check to see if the slot iss already full 
        if (isFull)
            return quantity;

        this.itemName = itemName;
        this.itemSprite = sprite;
        this.itemDescription = itemDescription;
        
        itemImage.sprite = itemSprite;

        this.quantity += quantity;
        if(this.quantity > maxNumberItems){
            quantityText.text = maxNumberItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            int extraItems = this.quantity-maxNumberItems;
            this.quantity = maxNumberItems;
            return extraItems;
        } else{
            quantityText.text = this.quantity.ToString();
            quantityText.enabled = true;
            isFull = false;
            return 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        Debug.Log("clicked");

        if(eventData.button == PointerEventData.InputButton.Left)
        {   
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();

        }

    }
    public void OnLeftClick()
    {   
        if(thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if(usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if(this.quantity <=0)
                    EmptySlot();

            }
        } else{
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if(itemDescriptionImage.sprite == null){
                itemDescriptionImage.sprite = emptySprite;
            }
        }
    }

    private void EmptySlot()
    {
        quantityText.enabled=false;
        itemImage.sprite = emptySprite;
        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
        itemName = "";
    }

    private void OnRightClick()
    {
        Vector3 position = GameObject.FindWithTag("Player").transform.position +new Vector3(3,0,0);

        inventoryManager.instantiateItem((itemName),1,position);
        /*
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;
        */
        //create and modify the SR

        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if(this.quantity <=0)
            EmptySlot();

    }


}
