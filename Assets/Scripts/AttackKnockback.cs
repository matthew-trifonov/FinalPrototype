using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKnockback : MonoBehaviour
{
    public float power;
    public float duration;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player"))
        {
            Rigidbody2D target = collider.GetComponent<Rigidbody2D>();

            if (target != null)
            {
                Vector2 distance = target.transform.position - transform.position;
                distance = distance.normalized * power;
                target.AddForce(distance, ForceMode2D.Impulse);
                if (collider.gameObject.CompareTag("Enemy") && collider.isTrigger)
                {
                    target.GetComponent<LogEnemy>().currentState = EnemyState.stagger;
                    target.GetComponent<LogEnemy>().Knockback(target, duration, damage);
                }
                if (collider.gameObject.CompareTag("Player"))
                {
                    if(collider.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        target.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        target.GetComponent<PlayerMovement>().Knockback(duration, damage);
                    }  
                }
                
                
            }
        }
    }
}
