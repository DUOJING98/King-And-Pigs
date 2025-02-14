using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;

    public float startTime;
    public float waitTime;
    public float boomForce;

    [Header("check")]
    public float radius;
    public LayerMask targetLayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        col = GetComponent<Collider2D>();
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Bomb_idle"))
            if (Time.time > startTime + waitTime)
            {
                anim.Play("Bomb_explotion");
            }

    }

    public void TurnOn()
    {
        startTime = Time.time;
        anim.Play("Bomb_on");
        if (Time.time > startTime + waitTime)
        {
            anim.Play("Bomb_explotion");
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Explotion()
    {
        col.enabled = false;
        Collider2D[] aroundObject = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        rb.gravityScale = 0;
        foreach (var item in aroundObject)
        {
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * boomForce, ForceMode2D.Impulse);
            
            if (item.CompareTag("Player") || item.CompareTag("Enemy"))
                item.GetComponent<IDamageable>().GetHit(3);


            if (item.CompareTag("Box"))
                item.GetComponent<Box>().DestroyBox();
        }
    }

    public void Destroythis()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Bomb_on"))
            if (collision.CompareTag("HitPoint"))
            {
                TurnOn();

            }
    }
    public void TurnOff()
    {
        anim.Play("Bomb_idle");
    }





}
