using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    private Animator anim;
    public bool isGrounded;
    private Rigidbody2D rb;
    public float jumpHeight = 5f;
    public int health;
    public Bt_enemy BT;
    public St_enemy ST;
    public LL_enemy LL;
    private bool areCollidingBt;
    private bool areCollidingSt;
    private bool areCollidingLL;
    private GameObject[] taggedObjects;
    //[SerializeField]
    //private GameObject goBt;
    //[SerializeField]
    //private GameObject goSt;
    //public GameObject BTT;
    //public float jumpForce = 2000f;

    //void Awake()
    //{
        //if (GameObject.Find("Bt_Enemy") != null)
        //{
            //BT = GameObject.FindGameObjectWithTag("BEnemy").GetComponent<Bt_enemy>();
        //}
        //if (GameObject.Find("St_Enemy") != null)
        //{
           // ST = GameObject.FindGameObjectWithTag("SEnemy").GetComponent<St_enemy>();
        //}
        //if (GameObject.Find("LL_Enemy") != null)
       // {
        //    LL = GameObject.FindGameObjectWithTag("LEnemy").GetComponent<LL_enemy>();
        //}
    //}

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKey (KeyCode.D))
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }

        if(Input.GetKey (KeyCode.A))
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpHeight);
                anim.SetBool("isJumping", true);
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack1");
            //areCollidingBt = false;
            //if(areCollidingBt)
            //{
            //    TakeDamageBt(1);
            //}
        }
        if(Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("attack2");
            //areCollidingBt = false;
            //if (areCollidingBt)
            //{
                //TakeDamageBt(2);
            //}
        }

        

    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }

        if (other.gameObject.tag == "Ground1")
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }

        if (other.gameObject.tag == "BEnemy")
        {
            areCollidingBt = true;
        }

        if (other.gameObject.tag == "SEnemy")
        {
            areCollidingSt = true;
        }

        if (other.gameObject.tag == "LEnemy")
        {
            areCollidingLL = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

        if (other.gameObject.tag == "BEnemy")
        {
            areCollidingBt = false;
        }

        if (other.gameObject.tag == "SEnemy")
        {
            areCollidingSt = false;
        }

        if (other.gameObject.tag == "LEnemy")
        {
            areCollidingLL = false;
        }


    }
   
    public void TakeDamage(int damage)
    {
        health = health - damage;
        anim.SetTrigger("isHurt");
        if (health <= 0)
        {
            anim.SetTrigger("Dead");
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamageBt(int damage)
    {
        if(areCollidingBt)
        {
            BT.health = BT.health - damage;
            if (BT.health <= 0)
            {
                BT.BSTrig();
            }
        }
    }

    public void TakeDamageSt(int damage)
    {
        if (areCollidingSt)
        {
            ST.health = ST.health - damage;
            if (ST.health <= 0)
            {
                taggedObjects = GameObject.FindGameObjectsWithTag("Projectile");
                for(int i = 0; i < taggedObjects.Length; i++)
                {
                    Destroy(taggedObjects[i]);
                }
                ST.SSTrig();
            }
        }
    }

    public void TakeDamageLL(int damage)
    {
        if (areCollidingLL)
        {
            LL.health = LL.health - damage;
            if (LL.health <= 0)
            {
                LL.LSTrig();
            }
        }
    }


}
