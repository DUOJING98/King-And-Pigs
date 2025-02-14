using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D coll;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        
        GameManager.Instance.isDoor(this);

        coll.enabled = false;
    }

    public void OpenDoor()//to GameManager
    {
        anim.Play("open");
        coll.enabled = true;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.NextScene();
        }
    }
}
