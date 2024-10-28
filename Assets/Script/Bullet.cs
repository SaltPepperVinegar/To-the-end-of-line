using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public float speed = 50f; // Bullet speed
    private Vector2 moveDirection;

    public float damage =1f;
    private Rigidbody2D rb;
    // Set the direction for the bullet
    public void Start(){
        moveDirection = transform.up;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*speed;
        //Debug.Log(rb.velocity);
        Destroy(gameObject, 10f);

    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy"){
            Debug.Log("damage");
            collision.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }
        Destroy(gameObject);

    }
}
