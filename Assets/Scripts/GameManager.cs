using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool _isGameActive = false;
    private bool _isGameOver = false;

    public GameObject player;
    public Image menu;
    public Image gameOver;
    public TextMeshProUGUI lastScore;

    public bool isGameOver
    {
        get { return _isGameOver; }
    }
    public bool isGameActive
    { 
        get { return _isGameActive; } 
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

        if (isGameActive && !isGameOver &&Input.GetKeyDown(KeyCode.Return))
        {
            // The "Enter" key has been pressed
            // Do something here, like print a message
            Debug.Log("Game Over");
            GameOver();
        }

        if (isGameOver)
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
        _isGameActive = true;
        score = 0;
        UpdateScore(0);
    }

    public void GameOver()
    {
        
        Debug.Log("Game Over!  -- Score = "+ score);
        
        _isGameActive = false;
        _isGameOver = true;
        lastScore.text = "Score: " + score;
        gameOver.gameObject.SetActive(true);
        Debug.Log("GameOver isGameActive = "+_isGameActive);


    }

    public void RestartGame()
    {
        // Reset the game state to the beginning
        gameOver.gameObject.SetActive(false);
        SceneManager.LoadScene("Gameplay");

    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        //Debug.Log(score);
    }

    
}
