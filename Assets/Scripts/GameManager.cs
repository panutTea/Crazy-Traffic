using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    private int nextLevel = 100;
    public bool isGameActive { get; private set; }
    public bool isGameOver { get; private set; }
    public int level { get; private set; }


    public GameObject player;
    public Image gameOver;
    public RawImage startGameText;
    public TextMeshProUGUI lastScore;
    public TextMeshProUGUI ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (score > nextLevel)
        {
            level++;
            nextLevel += 100*level;
            Debug.Log("Level: "+level+" next level: "+nextLevel);
        }
        if (isGameActive && !isGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            // The "Enter" key has been pressed
            // Do something here, like print a message
            Debug.Log("Game Over");
            GameOver();
        }

        else if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // The "Enter" key has been pressed
                // Do something here, like print a message
                Debug.Log("Restart Game");
                RestartGame();
            }
        }
    }

    public void StartGame()
    {
        //isGameActive = true;
        isGameOver = false;
        level = 1;
        score = 0;
        UpdateScore(0);
    }
    public void SetGameAvtive()
    {
        isGameActive = true;
        startGameText.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        
        Debug.Log("Game Over!  -- Score = "+ score);
        
        isGameActive = false;
        isGameOver = true;
        lastScore.text = score.ToString();
        gameOver.gameObject.SetActive(true);
        Debug.Log("GameOver isGameActive = "+ isGameActive);


    }

    public void RestartGame()
    {
        // Reset the game state to the beginning
        gameOver.gameObject.SetActive(false);
        SceneManager.LoadScene("City Scene");

    }
    public void BackToMenu()
    {
        // Reset the game state to the beginning
        gameOver.gameObject.SetActive(false);
        SceneManager.LoadScene("Game Menu");

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreText.text = "Score: "+score;
    }

    
}
