using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int totalScore;
    private int highScore;
    public static GameController instance;
    public Text scoreText;

    public GameObject gameOverPanel;

    public GameObject apples;

    private int appleCount;
    private int applesTotalLose;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        highScore = PlayerPrefs.GetInt("Score");

        scoreText.text = highScore.ToString();

        foreach (Transform child in apples.transform)
        {
            appleCount++;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void UpdateScoreText()
    {
        highScore++;
                
        scoreText.text = highScore.ToString();

        PlayerPrefs.SetInt("Score", highScore);
    }

    public void ShowGameOver()
    {
        highScore -= totalScore;

        PlayerPrefs.SetInt("Score", highScore);

        gameOverPanel.SetActive(true);
    }

    public void RestartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LostScore()
    {

        if (appleCount > 0 && highScore > 0)
        {
            int lostScore = highScore - appleCount;

            if (lostScore < 0)
            {
                lostScore = 0;
            }

            PlayerPrefs.SetInt("Score", lostScore);
        }
    }

}
