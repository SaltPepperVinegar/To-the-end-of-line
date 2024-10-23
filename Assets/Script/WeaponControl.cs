using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public int weaponType = 0;   // Starting weapon type
    public int maxWeaponType = 4; // Maximum weapon type
    public int minWeaponType = 0; // Minimum weapon type

    public int[] maxWeaponAmmo;

    private int[] currentWeaponAmmo;  

    private Animator animator;

    private void Start(){
        animator = GetComponent<Animator>();
        animator.SetFloat("weaponType",weaponType);
        currentWeaponAmmo = maxWeaponAmmo;
    }


    public void bulletShooted(int num){
        currentWeaponAmmo[weaponType] -= num;
    }
    public bool checkAmmunition(){
        return currentWeaponAmmo[weaponType]<= 0 ;
    }

    void Update()
    {   
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

    void Reload()
    {
        currentWeaponAmmo[weaponType] = maxWeaponAmmo[weaponType];
    }


}