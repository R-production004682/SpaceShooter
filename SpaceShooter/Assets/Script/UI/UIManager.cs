using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Constant;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] private Image livesImage;
    [SerializeField] private List<Sprite> liveSprites;

    [SerializeField] private GameManager gameManager;

    private int totalScore = 0;

    private void Start()
    {
        scoreText.text = $"Score : {0}";
        livesImage.sprite = liveSprites[DamageLevel.ALL_GREEN];
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = $"Score : {totalScore}";
    }

    public void UpdateLivesUI(int currentLives)
    {
        livesImage.sprite = liveSprites[currentLives];

        // 機体損傷度が全壊だったらGameOver
        if(currentLives == DamageLevel.SERIOUS)
        {
            gameManager.GameOver();
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);

            // GameOverTextを点滅させる
            StartCoroutine(GameOverFlickerRoutine());
        }
    }

    private IEnumerator GameOverFlickerRoutine()
    {
        while(true) { 
            gameOverText.text = "GAME OVER...";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
