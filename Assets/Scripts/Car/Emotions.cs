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

	private Cars carScript;

	// Start is called before the first frame update
	void Start()
	{
		// Set maximum value of emotion bar
		emoBar.maxValue = EMO_MAXIUM;
		
		emoBar.onValueChanged.AddListener(OnEmoValuesChange);

		carScript = GetComponentInParent<Cars>();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateEmotion(carScript.moveState == MoveStates.Stop);
	}

	void UpdateEmotion(bool isIncrease) {
		// If car get furious will not increase or decrease emotion value
		if (emoState == EmoStates.Furious && carScript.moveState == MoveStates.Stop) 
		{
			carScript.Moving();
		}
		else if (emoState != EmoStates.Furious)
		{
			if (isIncrease)
			{
				emoBar.value += emoIncreasingValue * Time.deltaTime;
			}
			else if (emoBar.value > 0)
			{
				emoBar.value += emoDecreasingValue * Time.deltaTime;
			}
		}
	}
	
	void OnEmoValuesChange(float value) 
	{
		Debug.Log("Check");
		// Check if emotion change or not
		for (int i = emoStateValueList.Length - 1; i >= 0; i--) {
			if (value >= emoStateValueList[i] * EMO_MAXIUM) {
				emoState = (EmoStates) i;
				emoji.GetComponent<SpriteRenderer>().sprite = emojiIcons[i];
				return;
			}
		}
	}
}
