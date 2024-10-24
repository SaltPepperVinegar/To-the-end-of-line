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
    public int AddItem(string itemName, int quantity, Sprite sprite, String itemDescription, int maxNumberItems)
    {   //Check to see if the slot iss already full 
        if (isFull)
            return quantity;

        this.itemName = itemName;
        this.itemSprite = sprite;
        this.itemDescription = itemDescription;
        this.maxNumberItems = maxNumberItems;

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
    public int removeItem(int quantity){
        isFull = false;
        if( this.quantity>= quantity)
        {
            this.quantity -= quantity;
            quantityText.text = this.quantity.ToString();
            if(this.quantity <=0)
                EmptySlot();
            return 0;
        }
        else{
            int leftOver = quantity - this.quantity;
            this.quantity = 0;
            quantityText.text = this.quantity.ToString();
            EmptySlot();
            return leftOver;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {   

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
                removeItem(1);
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
        quantity = 0;
    }

    private void OnRightClick()
    {
        Vector3 position = GameObject.FindWithTag("Player").transform.position +new Vector3(3,0,0);

        inventoryManager.instantiateItem((itemName),1,position);
        removeItem(1);

    }

}
