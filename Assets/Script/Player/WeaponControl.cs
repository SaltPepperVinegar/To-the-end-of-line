using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;

public class WeaponControl : MonoBehaviour
{
    public int weaponType = 0;   // Starting weapon type
    public int maxWeaponType = 4; // Maximum weapon type
    public int minWeaponType = 0; // Minimum weapon type

    [SerializeField] private TextMeshProUGUI AmmoStates;
    public AudioSource reloadAudio;

    public int[] reloadWeaponAmmo;

    public int[] totalWeaponAmmo;
    private int[] currentWeaponAmmo;  

    private Animator animator;
    private InventoryManager inventoryManager;
    private void Start(){
        animator = GetComponent<Animator>();
        animator.SetFloat("weaponType",weaponType);
        currentWeaponAmmo = new int[3];
        //Array.Copy(reloadWeaponAmmo, currentWeaponAmmo,3);
        for (int i=0;i<3 ;i++){
            currentWeaponAmmo[i] = 0;
        }
        
        inventoryManager = GetComponentInChildren<InventoryManager>();
    }


    public void bulletShooted(int num){
        currentWeaponAmmo[weaponType] -= num;
        switch(weaponType){
            case 0:
                inventoryManager.DeleteItem("RiffleAmmo", num);
                break;
            case 1:
                inventoryManager.DeleteItem("ShotGunAmmo", num);
                break;
            case 2:
                inventoryManager.DeleteItem("PistolAmmo", num);
                break;
        }
    }

    public bool checkAmmunition(){
        return currentWeaponAmmo[weaponType] > 0 ;
    }

    void Update()
    {   if (weaponType <3){
            AmmoStates.text = "AMMO "+ currentWeaponAmmo[weaponType] + "/" + totalWeaponAmmo[weaponType];
        } else{
            AmmoStates.text = "AMMO ";
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")||animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
        
                    // Get the scroll wheel input
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Scroll up (positive value)
            if (scrollInput > 0f)
            {   

                weaponType++ ;
                if (weaponType > maxWeaponType)
                {
                weaponType = minWeaponType;
                }
                animator.SetFloat("weaponType",weaponType);
            }
            // Scroll down (negative value)
            else if (scrollInput < 0f)
            {
                weaponType--;
                if (weaponType < minWeaponType)
                {
                    weaponType = maxWeaponType;
                }
                animator.SetFloat("weaponType",weaponType);

            }

            // Optional: Display current weaponType in the console for debugging

        }
    }

    public void Reload()
    {   
        Debug.Log("reload");
        int needToReload = reloadWeaponAmmo[weaponType] -  currentWeaponAmmo[weaponType];
        if(totalWeaponAmmo[weaponType] >= needToReload)
        {
            currentWeaponAmmo[weaponType] = reloadWeaponAmmo[weaponType];
            totalWeaponAmmo[weaponType] -= needToReload;

        } else{
            currentWeaponAmmo[weaponType] = totalWeaponAmmo[weaponType];
            totalWeaponAmmo[weaponType] = 0;
        }
    }

    public void reloadSound()
    {
        reloadAudio.Stop();
        reloadAudio.Play();
    }
    public void collectAmmo(int amount, int Ammotype){
        totalWeaponAmmo[Ammotype] += amount;
    }


}