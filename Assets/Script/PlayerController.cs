using System.Collections;
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
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsMoving",isMoving);

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
            
        if (GetComponent<WeaponScroll>().weaponType <3){
                if (Input.GetMouseButtonDown(0))
                {        
                    animator.SetTrigger("ShootTrigger");
                    Shoot();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    animator.SetTrigger("ReloadTrigger");
                    Reload();
                }

        }
        if (Input.GetMouseButtonDown(1)){
            animator.SetTrigger("MeleeTrigger");
            Melee();

        }
        animator.SetBool("IsMoving",isMoving);


    }
    
    void Shoot()
    {
        // Instantiate the bullet at the fire point position with the same rotation as the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }

    void Reload()
    {

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
}
