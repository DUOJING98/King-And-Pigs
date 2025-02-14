using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool canJump;
    private Boom Boom;

    public float MoveSpeed;
    public float jumpForce;
    public float AddForce;

    [Header("Player State")]
    public int health;
    public bool isDead;

    [Header("Ground Check")]
    public Transform GroundCheck;
    public float groundChecckRadius;
    public LayerMask groundLayer;
    public LayerMask targetLayer;

    [Header("State Check")]
    public bool isGround;
    public bool isJump;
    public bool isAttack;

    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;
    public GameObject runFX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Boom = GetComponent<Boom>();
        anim = GetComponent<Animator>();

        GameManager.Instance.IsPlayer(this);
        if (GameManager.Instance != null)
        {
            health = GameManager.Instance.LoadHealth();
        }
        UIManager.Instance.UpdateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {

            runFX.SetActive(false);
            return;//如果死了，不执行下面的指羴E
        }

        CheckInput();
    }

    public void FixedUpdate()
    {
        if (isDead)
        {
            //如果玩家死亡，速度归羴E
            rb.velocity = Vector2.zero;
            return;
        }
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround)
            canJump = true;

        if (Input.GetButtonDown("Attack") && !isAttack)
        {
            Attack();
            isAttack = true;
        }
    }
    void Movement()
    {
        //获取玩家输葋E
        float horizontalInput = Input.GetAxisRaw("Horizontal");//-1~1
        //设置水平速度
        rb.velocity = new Vector2(horizontalInput * MoveSpeed, rb.velocity.y);
        //反转player朝蟻E   
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }

        if (Mathf.Abs(rb.velocity.x) > 0.1)
        {
            runFX.SetActive(true);
        }
        else
        {
            runFX.SetActive(false);
        }
    }


    void Jump()
    {
        if (canJump)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 4;
            canJump = false;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0.125f, -0.08f, 0);
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void AttackOver()
    {
        isAttack = false;
    }

    //击退效箒E
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") || collider.CompareTag("Boom"))
        {

            Vector3 pos = transform.position - collider.transform.position;
            collider.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * AddForce, ForceMode2D.Impulse);
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<IDamageable>().GetHit(1);
            }
        }


    }
    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position, groundChecckRadius, groundLayer);
        if (isGround)
        {
            rb.gravityScale = 1;
            isJump = false;
        }
    }



    public void LandFX()
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0.103f, -0.27f, 0);
    }
    private void OnDrawGizmos()
    {
        if (GroundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GroundCheck.position, groundChecckRadius);
        }
    }
    public void Health(int amount)
    {
        health += amount;
        if (health > 3) health = 3; // 假设最大血量为3
        UIManager.Instance.UpdateHealth(health);
        GameManager.Instance.SaveData(); // 立即保存新血量
    }

    public void GetHit(int damage)
    {
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("Player_Hit"))
        {
            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            anim.SetTrigger("hit");

            UIManager.Instance.UpdateHealth(health);
        }
    }
}
