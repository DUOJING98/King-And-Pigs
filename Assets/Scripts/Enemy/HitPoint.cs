using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    int dir;
    public bool hitBomb;
    Boom boom;

    private void Start()
    {
        boom=GetComponent<Boom>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x)
            dir = -1;
        else
            dir = 1;
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir,1)*2,ForceMode2D.Impulse);
            collision.GetComponent<IDamageable>().GetHit(1);
        }

        if(collision.CompareTag("Boom"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 1) * 3, ForceMode2D.Impulse);
            
        }
    }
}
