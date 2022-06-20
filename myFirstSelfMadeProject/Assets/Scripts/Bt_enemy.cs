using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bt_enemy : Enemy 
{

    private Animator anim;
    public int damage1;
    private Player pl;
    private Vector2 Xpos;
    private Animator playerAnim;
    public float timeBetweenTeleports;
    private float teleportTime;
    public GameObject[] Tpoints;
    private Animator Btanim;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        teleportTime = timeBetweenTeleports;
        Btanim = gameObject.GetComponent<Animator>();
        Tpoints = GameObject.FindGameObjectsWithTag("Tpoint");
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player != null)
        {
            if(Vector2.Distance(transform.position, player.position) >= 22f)
            {
                Xpos = new Vector2(player.position.x, 0);
                transform.position = Vector2.MoveTowards(transform.position, Xpos, speed * Time.deltaTime);
                anim.SetBool("isClose", false);
            }
            else
            {
                anim.SetBool("isClose", true);
            }

            if(Time.time >= teleportTime)
            {
                int randNum;
                GameObject selPoint;
                randNum = Random.Range(0, 8);
                selPoint = Tpoints[randNum];

                transform.position = selPoint.transform.position;

                teleportTime = Time.time + timeBetweenTeleports;
            }
        }
    }

    public void TakeDamagePlBt(int damage)
    {
        //damage = damage1;
        pl.TakeDamage(damage);
    }

    public void BSTrig()
    {
        Btanim.SetTrigger("Dead");
        
    }

    public void BDeath()
    {
        Destroy(gameObject);
    }
}
