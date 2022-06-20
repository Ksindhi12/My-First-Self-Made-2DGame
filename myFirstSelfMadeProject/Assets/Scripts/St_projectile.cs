using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St_projectile : MonoBehaviour
{
    public float speed;
    public GameObject explosion;
    private Player pl;
    private Vector2 targetPosition;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = pl.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
       
    }
    
    void DestroyProjectile()
    {
        Destroy(gameObject);
        //Instantiate(explosion, transform.position, Quaternion.identity);
    }

    void ProjectileExplosion()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pl.TakeDamage(damage);
            ProjectileExplosion();
        }
        else
        {
            DestroyProjectile();
        }
    }
}
