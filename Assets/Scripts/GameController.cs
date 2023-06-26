using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public void GameOver(string loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }

    public void RestartGame()
    {
        Application.Quit();
    }


}
