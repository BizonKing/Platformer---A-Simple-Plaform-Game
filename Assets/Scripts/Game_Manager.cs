using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Takes care of the score and the game state (is the game starting? over?)
 * 
 * NOTE: Poor design choice of mines here is to have the game manager also take care of the UI elements of the
 *       start, score, and game over screens.
 */
public class Game_Manager : MonoBehaviour {

    public int score;
    public int scoreValue;
    public GameObject player;
    public Lava lava;
    public bool gameOver = false;
    public int gameCountdownValue = 5;

    // UI Stuff
    // Beginning Screen
    public GameObject beginningScreen;
    public Text timer;

    // Score Screen
    public GameObject scoreScreen;
    public Text scoreText;

    // Game Over Screen
    public GameObject gameOverScreen;
    public Text gameOverScoreText;
    public Button restartButton;
    public Button quitButton;
    // End of UI Stuff

    void Awake()
    {
        score = 0;
        player = GameObject.Find("Player");
        StartCoroutine(CountdownToStartGame());
    }

    void Update()
    {
        if (player == null && !gameOver)
        {
            EndGame();
        }
        else
        {
            scoreText.text = score.ToString();
        }
    }

    IEnumerator CountdownToStartGame()
    {
        int currentGameCountdownValue = gameCountdownValue;
        while (currentGameCountdownValue > 0)
        {
            timer.text = currentGameCountdownValue.ToString();
            yield return new WaitForSeconds(1.0f);
            currentGameCountdownValue--;
        }

        StartGame();
    }

    void StartGame()
    {
        beginningScreen.SetActive(false);
        scoreScreen.SetActive(true);
        player.GetComponent<Player>().SetCanMove(true);
        lava.StartLavaFlow();
    }

    void EndGame()
    {
        scoreScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScoreText.text = score.ToString();
        lava.StopLavaFlow();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    // Score is determined by the number of platforms the player touched
    public void UpdateScore(GameObject platformObj)
    {
        Platform platform = platformObj.GetComponent<Platform>();
        
        if (platform.GetTouchedByPlayer())
        {
            return;
        }
        else
        {
            score += scoreValue;
        }

    }

    public int GetScore()
    {
        return score;
    }

}
