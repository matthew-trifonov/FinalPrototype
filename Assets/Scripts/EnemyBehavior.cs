using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}
public class EnemyBehavoir : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyState currentState;
    public string type;
    public FloatValue maxHp;
    public float hp;
    public int attackDmg;
    public float speed;

    private void Awake()
    {
        hp = maxHp.value;
    }

    private void TakeDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            this.gameObject.SetActive(false);
        }

    }
    public void Knockback(Rigidbody2D target, float duration, float damage)
    {
        StartCoroutine(KnockbackTimer(target, duration));
        TakeDamage(damage);
    }
    private IEnumerator KnockbackTimer(Rigidbody2D target, float duration)
    {
        if (target != null)
        {
            Debug.Log(duration);
            yield return new WaitForSeconds(duration);
            target.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            target.velocity = Vector2.zero;
        }
    }
}
