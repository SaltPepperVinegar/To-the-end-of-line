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


    private Animator animator;

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
        if(!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x*unitMovement;
                targetPos.y += input.y*unitMovement;
        

                StartCoroutine(Move(targetPos));
            }
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
        while ((targetPos-transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

    }
}
