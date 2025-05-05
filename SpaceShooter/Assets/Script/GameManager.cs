using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constant;

public class GameManager : MonoBehaviour
{
    private bool isGameOver;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            isGameOver = false;
            SceneManager.LoadScene(SceneNames.INGAME);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
