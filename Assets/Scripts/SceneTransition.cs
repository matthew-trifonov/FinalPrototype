using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string loadScene;
    public Vector2 position;
    public VectorValue storedLocation;
    public BoolValue sceneLoaded;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.CompareTag("Player") && !collider.isTrigger)
        {
            storedLocation.value = position;
            sceneLoaded.RuntimeValue = true;
            SceneManager.LoadScene(loadScene);

        }
    }
}
