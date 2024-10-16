using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
                targetPos.x += input.x*0.1f;
                targetPos.y += input.y*0.1f;
        

                StartCoroutine(Move(targetPos));
            }
        }
            if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }
    void Shoot()
    {
        // Instantiate bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.up = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized; // Make bullet face mouse position
        bullet.GetComponent<Rigidbody2D>().isKinematic = false; // Enable physics on the bullet
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
