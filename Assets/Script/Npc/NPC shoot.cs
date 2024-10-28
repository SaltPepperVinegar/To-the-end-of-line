using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCshoot : MonoBehaviour
{

    private GameObject _enemy = null;
    public bool readyToShoot = true;
    private Animator animator;
    private AllyNpc NPC;
    public GameObject bulletPrefab;

    public Transform firePoint;

    public AudioSource shootAudio;

    void Start(){
        NPC = GetComponentInParent<AllyNpc>();
        animator = GetComponentInParent<Animator>();

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"&& readyToShoot){
            Debug.Log("shoot");
            Vector2 moveDirection = (collision.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            NPC.transform.rotation = Quaternion.Euler(0, 0, angle);

            _enemy = collision.gameObject;

            animator.SetTrigger("ShootTrigger");
            StartCoroutine(RechargeShoot());
        }
 
    }

    IEnumerator RechargeShoot(){
        readyToShoot = false;
        yield return new WaitForSeconds(0.25f);
        Vector2 moveDirection = (_enemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        NPC.transform.rotation = Quaternion.Euler(0, 0, angle);

        shoot();
        yield return new WaitForSeconds(0.75f);
        readyToShoot = true;
    }

    public void shoot(){
        shootAudio.Stop();
        shootAudio.Play();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }
}
