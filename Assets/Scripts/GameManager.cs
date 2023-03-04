using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool _isGameActive = true;
    private bool _isGameOver = false;


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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int difficulty)
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
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        //Debug.Log(score);
    }

    
}
