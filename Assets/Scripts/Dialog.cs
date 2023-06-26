using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string[] dialogLines; 
    public bool dialogActive;
    private int currentLine = 0;
    public bool isNPC;
    public BoolValue sceneLoaded;
    public BoolValue hasKey;

    private void Awake()
    {
        if (!isNPC && !sceneLoaded.RuntimeValue)
        {
            
            StartCoroutine(PlayDialogCoroutine());
        }
    }

    private IEnumerator PlayDialogCoroutine()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>().currentState = PlayerState.dialog;
        dialogActive = true;
        dialogBox.SetActive(true);

        for (int i = 0; i < dialogLines.Length; i++)
        {
            dialogText.text = dialogLines[i];

            bool spacePressed = false;
            while (!spacePressed)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    spacePressed = true;
                }

                yield return null;
            }
        }

        dialogActive = false;
        dialogBox.SetActive(false);
        player.GetComponent<PlayerMovement>().currentState = PlayerState.idle;
    }

    private void Update()
    {
        if (isNPC && dialogActive)
        {
            PerformDialog();
        }
    }

    public void PerformDialog()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (dialogBox.activeInHierarchy && currentLine >= dialogLines.Length)
            {
                player.GetComponent<PlayerMovement>().currentState = PlayerState.exitTime;
                dialogBox.SetActive(false);
                hasKey.RuntimeValue = true;
                currentLine = 0;
            }
            else
            {
                player.GetComponent<PlayerMovement>().currentState = PlayerState.dialog;
                dialogBox.SetActive(true);
                dialogText.text = dialogLines[currentLine];
                currentLine++;
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            dialogActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            dialogActive = false;
        }
    }
}
