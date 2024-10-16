using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet speed
    private Vector2 moveDirection;

    private Rigidbody2D rb;
    // Set the direction for the bullet
    public void Start(){
        moveDirection = transform.up;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*speed;
        Debug.Log(rb.velocity);
        Destroy(gameObject, 10f); // Destroy after 3 seconds

    }

    void OnCollisionEnter2D() {
        Destroy(gameObject);
    }
}
