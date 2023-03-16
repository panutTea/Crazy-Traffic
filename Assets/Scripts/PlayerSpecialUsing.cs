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
	[SerializeField] private AudioClip hallsCoolSound;
	private AudioSource playerAudio;
	
	[System.Serializable]
	public class UnityStringEvent : UnityEvent<string> { }
	public UnityStringEvent OnRecognized;
	
	private void Start()
	{
		playerAudio = GetComponent<AudioSource>();
	}
	
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
		if (hallsCoolNum > 0)
        {
			playerAudio.PlayOneShot(hallsCoolSound, 1.0f);
			OnRecognized.Invoke("useHallsCool");
			hallsCoolNum--;
        }
	}
}