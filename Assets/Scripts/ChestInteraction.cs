using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChestInteraction : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public bool chestActive;
    public string text;
    public BoolValue itemFound;
    public Animator animator;
    public BoolValue hasKey;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)  && chestActive == true && !itemFound.RuntimeValue)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerMovement>().currentState = PlayerState.dialog;
            Debug.Log(player.GetComponent<PlayerMovement>().currentState);
            if (dialogBox.activeInHierarchy)
            {
                if (hasKey.RuntimeValue)
                {
                    itemFound.RuntimeValue = true;
                }
                player.GetComponent<PlayerMovement>().currentState = PlayerState.exitTime;
                dialogBox.SetActive(false);

            }
            else
            {
                dialogBox.SetActive(true);
                if (!hasKey.RuntimeValue)
                {
                    dialogText.text = "Hmmm... maybe the locksmith has the key.";
                    
                }
                else
                {
                    AnimateChest();
                    dialogText.text = text;
                }
                
            }
        }
    }

    public void AnimateChest()
    {
        animator.SetBool("isOpen", true);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            chestActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            chestActive = false;
        }
    }
}
