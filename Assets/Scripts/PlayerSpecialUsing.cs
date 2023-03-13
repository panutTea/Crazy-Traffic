using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerSpecialUsing : MonoBehaviour
{
	[SerializeField] private int hallsCoolNum = 0;
	[SerializeField] private TextMeshProUGUI hallsCoolNumText;
	[SerializeField] private GameObject hallsCoolShowcase;
	
	[System.Serializable]
	public class UnityStringEvent : UnityEvent<string> { }
	public UnityStringEvent OnRecognized;
	
	public void AddHallsCool()
	{
		hallsCoolNum++;
	}
	
	private void Update()
	{
		hallsCoolNumText.text = hallsCoolNum.ToString();
		if (hallsCoolNum > 0 )
		{
			hallsCoolShowcase.SetActive(true);
		}
		else
		{
			hallsCoolShowcase.SetActive(false);
		}
	}
	
	public void UseHallsCool()
	{
		OnRecognized.Invoke("useHallsCool");
		hallsCoolNum--;
	}
}