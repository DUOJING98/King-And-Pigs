using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;

    public Animator anim;
    public int animtorState;
    private GameObject Sign;

    [Header("Enemy State")]
    public int health;
    public bool isDead;

    [Header("Movement")]
    public float patrolSpeed;
    public Transform PointA, PointB;
    public float waitTime;
    [Header("Attack Setting")]
    public float attackRate;
    public float attackRange, skillRange;
    public float nextAttack = 0;
    public List<Transform> attackList = new List<Transform>();

    public Transform targetPoint;
    //FSM
    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    // Start is called before the first frame update
    public virtual void Init()
    {
        
        anim = GetComponent<Animator>();
        Sign = transform.GetChild(0).gameObject;

    }

    public void Awake()
    {
        Init();
    }
    void Start()
    {
        GameManager.Instance.IsEnemy(this);//ÉùÃ÷µÐÈË
        TransitionState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("dead", isDead);
        if (isDead)
        {
            GameManager.Instance.EnemyDead(this);
            return;
        }
        currentState.OnUpdate(this);
        anim.SetInteger("State", animtorState);
    }

    public void TransitionState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void MoveToTarget()
    {

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);
        FilpDirection();
    }



    public void SwitchPoint()
    {
        if (Mathf.Abs(PointA.position.x - transform.position.x) > Mathf.Abs(PointB.position.x - transform.position.x))
        {
            targetPoint = PointA;
        }
        else
        {
            targetPoint = PointB;
        }
    }
    public void FilpDirection()
    {
        if (transform.position.x < targetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }



    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!attackList.Contains(collision.transform) && !isDead && !GameManager.Instance.GameOver)
        {
            attackList.Add(collision.transform);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }

    public void AttackAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {
                anim.SetTrigger("attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }


    public virtual void SkillAction()
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {

            if (Time.time > nextAttack)
            {
                anim.SetTrigger("skill");
                nextAttack = Time.time + attackRate;
            }


        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && !GameManager.Instance.GameOver)
            StartCoroutine(OnAlarm());
    }

    IEnumerator OnAlarm()
    {
        Sign.SetActive(true);
        yield return new WaitForSeconds(Sign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        Sign.SetActive(false);
    }

    
}
