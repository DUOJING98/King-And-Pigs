using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.SwitchPoint();
        enemy.animtorState = 0;
    }

    public override void OnUpdate(Enemy enemy)
    {
       
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))//如果不是idle动画
        {
            enemy.animtorState = 1;
            enemy.MoveToTarget();
        }


        if (Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x) < 0.01f)
        {
            enemy.TransitionState(enemy.patrolState);
        }

        if(enemy.attackList.Count > 0)
        {
            enemy.TransitionState(enemy.attackState);
        }

    }
}
