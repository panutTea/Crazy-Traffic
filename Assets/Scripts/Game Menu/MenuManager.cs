using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
	using UnityEditor;

#endif

public class MenuManager : MonoBehaviour
{

	public GameObject player;
	public GameObject menu;
	public GameObject guide;
	public GameObject toggle;
	public GameObject allPages;
	public GameObject nextBtn;
	public GameObject backBtn;
	public int guidePage = 1;
	private bool isHandMovement = false;
	public RawImage toggleBtnImage;
	public Texture enable;
	public Texture disable;

	// Start is called before the first frame update
	void Start()
	{
		toggleBtnImage.texture = isHandMovement ? enable : disable;
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
		//Debug.Log(guidePage);
		if (guidePage == 1)
		{
			backBtn.SetActive(false);
		}
		else
		{
			backBtn.SetActive(true);
		}
		if(guidePage == allPages.transform.childCount)
		{
			nextBtn.SetActive(false);
		}
		else
		{
			nextBtn.SetActive(true);
		}
	}

	public void StartGame()
	{
        if (isHandMovement)
        {
			SceneManager.LoadScene("City Scene Movement");
		}
        else
        {
			SceneManager.LoadScene("City Scene Button");
		}
		
	}

	public void ExitGame()
	{
		Debug.Log("Exit");
		
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
		#else
			Application.Quit();
		#endif
	}
	public void NextPage()
	{
		allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(false);
		guidePage += 1;
		allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(true);
	}
	public void OpenGuide()
	{
		menu.SetActive(false);
		guide.SetActive(true);
	}
	public void ExitGuide()
	{
		allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(false);
		guidePage = 1;
		allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(true);
		guide.SetActive(false);
		menu.SetActive(true);
	}
	public void BackPage()
	{
		if (guidePage == 1)
		{
			
		}
		else
		{
			allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(false);
			guidePage -= 1;
			allPages.transform.GetChild(guidePage - 1).gameObject.SetActive(true);
		}
	}

	public void ToggleMode()
    {
		isHandMovement = !isHandMovement;
		toggleBtnImage.texture = isHandMovement ? enable : disable;
	}
}
