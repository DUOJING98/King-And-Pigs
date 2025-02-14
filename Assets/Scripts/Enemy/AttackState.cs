using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    
    public override void EnterState(Enemy enemy)
    {
        enemy.animtorState = 1;
        enemy.targetPoint=enemy.attackList[0];
    }

    public override void OnUpdate(Enemy enemy)
    {
        if(enemy.attackList.Count ==0)
        {
             enemy.TransitionState(enemy.patrolState); 
        }

        if (enemy.attackList.Count > 1)
        {
            for (int i = 0; i < enemy.attackList.Count; i++)
            {
                if (Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].transform.position.x) <
                    Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
                {
                    enemy.targetPoint = enemy.attackList[i];
                }
            }
        }
       
        if (enemy.attackList.Count == 1)
        { enemy.targetPoint = enemy.attackList[0]; } 
        
        if(enemy.targetPoint.CompareTag("Player"))
        {
            enemy.AttackAction();
           
        }
        if(enemy.targetPoint.CompareTag("Boom"))
        {
            enemy.SkillAction();
        }
        
        enemy.MoveToTarget();
    }

  
}
