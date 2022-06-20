using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class St_enemy : Enemy
{

    private Vector2 Xpos;
    private Player pl;
    private Animator Stanim;
    private float attackTime;
    public float timeBetweenAttacks;
    public GameObject matrixBall;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Stanim = gameObject.GetComponent<Animator>();
        attackTime = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (Vector2.Distance(transform.position, player.position) >= 40f)
            {
                Xpos = new Vector2(player.position.x, 0);
                transform.position = Vector2.MoveTowards(transform.position, Xpos, speed * Time.deltaTime);
                
            }

            if(Time.time >= attackTime)
            {
                //ShootProjectile(matrixBall);
                Stanim.SetTrigger("shoot");
                attackTime = timeBetweenAttacks + Time.time;
            }
        }
    }

    public void SSTrig()
    {
        Stanim.SetTrigger("Dead");
    }

    public void SDeath()
    {
        Destroy(gameObject);
    }

    void ShootProjectile()
    {
        Instantiate(matrixBall, transform.position, Quaternion.identity);
    }
}
