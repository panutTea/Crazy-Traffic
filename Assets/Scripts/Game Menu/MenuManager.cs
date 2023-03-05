using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{

    public GameObject player;
    public Image menu;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // The "Enter" key has been pressed
            // Do something here, like print a message
            Debug.Log("Start Game");
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("City Scene");
    }

}
