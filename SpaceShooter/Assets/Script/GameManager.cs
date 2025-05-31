using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constant;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LaserPool laserPool;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private Player player;

    private bool isGameOver;

    private void Awake()
    {
        StartGameSequence();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            isGameOver = false;

            // LaserPool‚ð•Ð•t‚¯‚é
            foreach (var pool in FindObjectsOfType<LaserPool>())
            {
                Destroy(pool.gameObject);
            }

            SceneManager.LoadScene(SceneNames.MAIN);
        }
    }

    public void StartGameSequence()
    {
        StartCoroutine(GameStartRoutine());
    }

    private IEnumerator GameStartRoutine()
    {
        uiManager.ShowCountdown("3");
        yield return new WaitForSeconds(1f);
        uiManager.ShowCountdown("2");
        yield return new WaitForSeconds(1f);
        uiManager.ShowCountdown("1");
        yield return new WaitForSeconds(1f);
        uiManager.ShowCountdown("Go!!");
        yield return new WaitForSeconds(0.5f);
        uiManager.HideCountdown();


        spawnManager.StartAllSpawn();
        player.GetComponent<PlayerShooter>().EnableShooting();
        player.GetComponent<PlayerMovementController>().EnableControl();
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
