using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{

    public GameObject player;
    public GameObject menu;
    public GameObject guide;
    public GameObject allPages;
    public GameObject nextBtn;
    public int guidePage = 1;

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
        Debug.Log(guidePage);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("City Scene");
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void NextPage()
    {
        allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(false);
        guidePage += 1;
        allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(true);
        if (guidePage == allPages.transform.childCount)
        {
            nextBtn.SetActive(false);
        }

        //allPages.SetActive(false);
    }
    public void Guide()
    {
        menu.SetActive(false);
        guide.SetActive(true);
        //allPages.SetActive(false);
        //pages[guidePage - 1].SetActive(true);
    }
    public void BackPage()
    {
        if (guidePage == 1)
        {
            menu.SetActive(true);
            guide.SetActive(false);
        }
        else
        {
            if (guidePage == allPages.transform.childCount)
            {
                nextBtn.SetActive(true);
            }
            //allPages.SetActive(false);
            allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(false);
            guidePage -= 1;
            allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(true);
        }
    }
}
