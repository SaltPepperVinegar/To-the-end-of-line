using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Enemy _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {   
            _enemy.attacking= true;
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            _enemy.attacking= false;
        }
    }

}
