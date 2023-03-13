using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EmoStates 
{
	Happy,
	Good,
	Fine, 
	Anger,
	Furious
};
public class Emotions : MonoBehaviour
{
	public Slider emoBar;
	
	public GameObject emoji;
	
	public Sprite[] emojiIcons;

	[SerializeField]
	// A value to increase emotion value per second
	private float emoIncreasingValue = 5;
	// A value to decrease emotion value per second
	[SerializeField]
	private float emoDecreasingValue = -10;
	[SerializeField]
	private float EMO_MAXIUM = 100;

	// State of emotion (5 states)
	[SerializeField]
	private float[] emoStateValueList = {0, 0.25f, 0.5f, 0.75f, 1};

	private EmoStates emoState = EmoStates.Happy;

	private Car carScript;

	//Sound
	public AudioClip carHornAngrySound;
	private AudioSource emotionAudio;

	// Start is called before the first frame update
	void Start()
	{
		// Set maximum value of emotion bar
		emoBar.maxValue = EMO_MAXIUM;
		
		emoBar.onValueChanged.AddListener(OnEmoValuesChange);

		carScript = GetComponentInParent<Car>();

		emotionAudio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!carScript.isCrazy)
		{
			UpdateEmotion(carScript.moveState == MoveStates.Stop);
		}
			
	}

	void UpdateEmotion(bool isIncrease) {
		// If car get crash, it will get furious immediatly
		if (carScript.isCrash) 
		{
			emoBar.value = EMO_MAXIUM;
			emoState = EmoStates.Furious;
		}
		
		// If car get furious will not increase or decrease emotion value
		else if (emoState == EmoStates.Furious && carScript.moveState == MoveStates.Stop) 
		{
			carScript.isCrazy = true;
		}
		else if (emoState != EmoStates.Furious)
		{
			if (isIncrease)
			{
				emoBar.value += emoIncreasingValue * Time.deltaTime;
				if (emoState == EmoStates.Anger)
				{
					emotionAudio.PlayOneShot(carHornAngrySound, 1.0f);
				}
			}
			else if (emoBar.value > 0)
			{
				emoBar.value += emoDecreasingValue * Time.deltaTime;
			}
		}
	}
	
	void OnEmoValuesChange(float value) 
	{
		// Check if emotion change or not
		for (int i = emoStateValueList.Length - 1; i >= 0; i--) {
			if (value >= emoStateValueList[i] * EMO_MAXIUM) {
				emoState = (EmoStates) i;
				emoji.GetComponent<SpriteRenderer>().sprite = emojiIcons[i];
				return;
			}
		}
	}
	
	public void ClearCarEmotion()
	{
		emoBar.value = 0;
	}
}
