using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LL_enemy : Enemy
{
    private Vector2 Xpos;
    private Player pl;
    private Animator LLanim;
    private float attackTime;
    public float timeBetweenAttacks;
    private bool areCollidingLL;
    public float hitHeight;
    private Rigidbody2D rb;
    private Animator playerAnim;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LLanim = gameObject.GetComponent<Animator>();
        attackTime = timeBetweenAttacks;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) >= 30f)
            {
                Xpos = new Vector2(player.position.x, 0);
                transform.position = Vector2.MoveTowards(transform.position, Xpos, speed * Time.deltaTime);

            }

            if (Time.time >= attackTime)
            {
                StartCoroutine(attack());
                attackTime = timeBetweenAttacks + Time.time;
            }
            //else
            //{
                
                //LLanim.SetBool("isCharging", false);
            //}
        }
    }

    public void LSTrig()
    {
        LLanim.SetTrigger("Dead");
    }

    public void LDeath()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            areCollidingLL = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            areCollidingLL = false;
        }
    }

    IEnumerator attack()
    {
        LLanim.SetTrigger("isCharging");
        yield return new WaitForSeconds(2);
        LLanim.SetTrigger("hit");
        //LLanim.SetBool("isCharging", false);
        if(areCollidingLL)
        {
            int damage = 3;
            rb.AddForce(Vector2.up * hitHeight);
            pl.TakeDamage(damage);
            playerAnim.SetBool("isJumping", true);

        }
        yield return new WaitForSeconds(1);
        playerAnim.SetBool("isJumping", false);
        
    }

    //public void TakeDamagePlLL(int damage)
    //{
        //if(areCollidingLL)
        //{
            
        //}
            
    //}

}
