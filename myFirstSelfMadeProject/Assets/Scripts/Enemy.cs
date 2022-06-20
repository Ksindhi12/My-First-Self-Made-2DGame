using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    //public float damage;
    //public float timeBetweenAttacks;
    public float speed;
    //public GameObject deathAnim;

    [HideInInspector]
    public Transform player;

    
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            //initiate death animation
            Destroy(gameObject);
        }
    }
}
