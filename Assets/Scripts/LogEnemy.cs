using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LogEnemy : EnemyBehavoir
{
    private Rigidbody2D enemy;
    public Transform playerTarget;
    public float chaseRadius;
    public float attackRadius;
    public Transform home;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        enemy = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(playerTarget.position, transform.position) <= chaseRadius && Vector3.Distance(playerTarget.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger) 
            {
                Vector3 enemyPos = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
                updateAnimation(enemyPos - transform.position);
                enemy.MovePosition(enemyPos);
                ChangeState(EnemyState.walk);
                animator.SetBool("isAwake", true);
            }

        }
        else if (Vector3.Distance(playerTarget.position, transform.position) > chaseRadius)
        {
            animator.SetBool("isAwake", false);
        }
    }
    public void updateAnimation(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) 
        {
            animator.SetFloat("changeY", 0);
            if (direction.x > 0)
            {
                animator.SetFloat("changeX", 1);
            }
            else if(direction.y > 0)
            {
                animator.SetFloat("changeX", -1);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            animator.SetFloat("changeX", 0);
            if(direction.y > 0)
            {
                animator.SetFloat("changeY", 1);
            }
            else if(direction.y < 0)
            {
                animator.SetFloat("changeY", -1);
            }
        }

    }
    private void ChangeState(EnemyState state)
    {
        if(currentState != state) 
        {
            currentState = state;
        }
    }
}
