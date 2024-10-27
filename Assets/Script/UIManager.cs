using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool inventoryMenuActivated = false;

    public bool onUI = false;
    

    void Update()
    {   
        if (Input.GetButtonDown("Inventory")){
            InventroyControl();
        }

    }

    private void InventroyControl(){
        if (inventoryMenuActivated)
        {
            Time.timeScale =1;  
            InventoryMenu.SetActive(false);
            inventoryMenuActivated = false;
            onUI = false;

        } else if (!inventoryMenuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            inventoryMenuActivated = true;
            onUI = true;


        }
    }
    public void DeathScene()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-1);
    }
}
