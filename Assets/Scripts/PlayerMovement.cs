using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle, 
    dialog,
    exitTime
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    private Rigidbody2D playerRigidBody;
    private Vector3 movementChange;
    public float playerSpeed;
    private Animator anim;
    public FloatValue currentHp;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public BoolValue hasKey;
    public BoolValue hasItem;
    public BoolValue itemPickUp;
    public Sprite key;
    public Sprite itemSprite;
    public BoolValue pickedUp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        anim.SetFloat("changeX", 0);
        anim.SetFloat("changeY", -1);
        transform.position = startingPosition.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != PlayerState.dialog)
        {
           
            movementChange = Vector3.zero;
            movementChange.x = Input.GetAxisRaw("Horizontal");
            movementChange.y = Input.GetAxisRaw("Vertical");
            if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.exitTime)
            {
                StartCoroutine(AttackCoroutine());
            }
            else if (currentState == PlayerState.walk || currentState == PlayerState.idle || currentState == PlayerState.exitTime)
            {
                AnimateMovement();
            }
        }
        else
        {
            movementChange = Vector3.zero;
            AnimateMovement();
        }

        if(hasKey.RuntimeValue && !pickedUp.RuntimeValue)
        {
            StartCoroutine(PickUpCoroutine());
        }
        else if(hasItem.RuntimeValue && !itemPickUp.RuntimeValue)
        {
            StartCoroutine(PickUpCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {

        anim.SetBool("isAttacking", true);
        currentState = PlayerState.attack;
        yield return null;
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.2f);
        currentState = PlayerState.walk;
 
    }

    private IEnumerator PickUpCoroutine()
    {
        Debug.Log("test");
        GameObject item = GameObject.FindWithTag("Item");
        anim.SetBool("hasItem", true);
        if(!hasItem.RuntimeValue)
        {
            item.GetComponent<SpriteRenderer>().sprite = key;
        }
        else
        {
            item.GetComponent<SpriteRenderer>().sprite = itemSprite;
            itemPickUp.RuntimeValue = true;
        }
        yield return new WaitForSeconds(0.6f);
        item.GetComponent<SpriteRenderer>().sprite = null;
        anim.SetBool("hasItem", false);
        pickedUp.RuntimeValue = true;

    }

    void AnimateMovement()
    {
        if (movementChange != Vector3.zero)
        {
            MovePlayer();
            anim.SetFloat("changeX", movementChange.x);
            anim.SetFloat("changeY", movementChange.y);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if(currentState == PlayerState.exitTime) 
        {
            currentState = PlayerState.idle;
        }
    }

    void MovePlayer()
    {
        movementChange.Normalize();
        playerRigidBody.MovePosition(transform.position + movementChange * playerSpeed * Time.deltaTime);
    }

    public void Knockback(float duration, float damage)
    {
        currentHp.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHp.RuntimeValue > 0)
        {
            StartCoroutine(KnockbackTimer(duration));
        }
        else
        {
            this.gameObject.SetActive(false);
            GameController controller = new GameController();
            controller.GameOver("GameOver");

        }
        
    }
    private IEnumerator KnockbackTimer(float duration)
    {
        if (playerRigidBody != null)
        {
            yield return new WaitForSeconds(duration);
            playerRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRigidBody.velocity = Vector2.zero;
        }
    }
}
