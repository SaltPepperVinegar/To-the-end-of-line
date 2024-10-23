using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public GameObject bulletPrefab;

    public Transform firePoint;
    public float unitMovement;
    public bool isMoving;
    private Rigidbody2D rb;
    private Vector2 input;

    public LayerMask solidObjectsLayer;

    private Animator animator;

    [SerializeField] HealthBar healthbar;

    private Vector3 targetPos;

    public GameObject muzzleFlash;
    private WeaponControl weaponControl;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsMoving",isMoving);
        weaponControl = GetComponent<WeaponControl>();
        muzzleFlash.GetComponent<SpriteRenderer>().enabled =false;
    }
    
    void Update()
    {   
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        rb.velocity = input.normalized*moveSpeed;
        if (input != Vector2.zero)
        {
            isMoving = true;
        } else{
            isMoving = false;
        }
            
        if(weaponControl.weaponType <3){
            if (Input.GetKeyDown(KeyCode.R))
            {   
                animator.SetTrigger("ReloadTrigger");
            }   
            switch (weaponControl.weaponType){
                case 0: 
                case 1:
                    if (Input.GetMouseButton(0)  )
                    {   
                        if(weaponControl.checkAmmunition()){
                            animator.SetTrigger("ShootTrigger");
                        } else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Reload")){
                            animator.SetTrigger("ReloadTrigger");
                        }
                    }
                    break;
                default :
                    if (Input.GetMouseButtonDown(0)  )
                    {   
                        if(weaponControl.checkAmmunition()){
                            animator.SetTrigger("ShootTrigger");
                        } else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Reload")){
                            animator.SetTrigger("ReloadTrigger");
                        }
                    }
                break;
                    
            }

        }

        

        if (Input.GetMouseButtonDown(1)){
            animator.SetTrigger("MeleeTrigger");
            Melee();

        }
        
        animator.SetBool("IsMoving",isMoving);


    }
    
    public void ShootHandGun()
    {   
        if ( weaponControl.checkAmmunition())
        {// Instantiate the bullet at the fire point position with the same rotation as the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            weaponControl.bulletShooted(1);
            StartCoroutine(flash());

        }
    }
    public void ShootRifle()
    {
        if ( weaponControl.checkAmmunition())
        {// Instantiate the bullet at the fire point position with the same rotation as the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            weaponControl.bulletShooted(1);
            StartCoroutine(flash());

        }
    }
    public void ShootShotGun()
    {
    if (weaponControl.checkAmmunition())
    {
        int bulletCount = 5; // Number of bullets in the spread
        float spreadAngle = 20f; // Total angle for the spread (e.g., 30 degrees)
        StartCoroutine(flash());
        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate the angle for each bullet on the z-axis (for 2D top-down)
            float angleOffset = (i - (bulletCount - 1) / 2f) * (spreadAngle / (bulletCount - 1));

            // Create a rotation for the bullet with the calculated offset on the z-axis
            Quaternion bulletRotation = firePoint.rotation * Quaternion.Euler(0, 0, angleOffset);
            
            // Instantiate the bullet at the fire point position with the calculated rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);  
            bullet.GetComponent<Bullet>().damage = 0.5f;
            // Register that a bullet has been shot
        }
        weaponControl.bulletShooted(1);

    }
    }
    void Melee()
    {

    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos-transform.position).sqrMagnitude > 0.001f)
        {
            rb.velocity = (targetPos-transform.position).normalized*moveSpeed;
            yield return null;
        }
        transform.position = targetPos;
        rb.velocity = Vector2.zero;

        isMoving = false;

    }

    IEnumerator flash(){
        muzzleFlash.GetComponent<SpriteRenderer>().enabled =true;
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.GetComponent<SpriteRenderer>().enabled =false;

    }
}
