using UnityEngine;

public class WeaponScroll : MonoBehaviour
{
    public int weaponType = 0;   // Starting weapon type
    public int maxWeaponType = 4; // Maximum weapon type
    public int minWeaponType = 0; // Minimum weapon type

    private Animator animator;

    private void Start(){
        animator = GetComponent<Animator>();
        animator.SetFloat("weaponType",weaponType);

    }
    void Update()
    {
        // Get the scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Scroll up (positive value)
        if (scrollInput > 0f)
        {   
            Debug.Log(scrollInput);

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