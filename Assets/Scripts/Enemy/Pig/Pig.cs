using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy ,IDamageable
{
    public void GetHit(int damage)
    {
        health-=damage;
        if(health < 1 )
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
    }

   

     

    
}
