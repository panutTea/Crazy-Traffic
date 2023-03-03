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
	// public Text emoStateText;
	public GameObject emoji;
	public Sprite[] emojiIcons;

	private float emoValues;

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

	public EmoStates emoState = EmoStates.Happy;

	private Cars carScript;

	// Start is called before the first frame update
	void Start()
	{
		// Set maximum value of emotion bar
		emoBar.maxValue = EMO_MAXIUM;

		carScript = GetComponent<Cars>();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateEmotionValues(carScript.moveState == MoveStates.Stop);
	}

	void UpdateEmotionValues(bool isIncrease) {
		if (isIncrease) 
		{
			emoValues += emoIncreasingValue * Time.deltaTime;
		}
		else if (emoValues > 0)
		{
			emoValues += emoDecreasingValue * Time.deltaTime;
		}
		emoBar.value = emoValues;

		// Change state
		for (int i = 0; i < emoStateValueList.Length; i++) {
			if (emoValues >= emoStateValueList[i] * EMO_MAXIUM) {
				emoState = (EmoStates) i;
				// emoStateText.text = ((EmoStates) i).ToString();
				emoji.GetComponent<SpriteRenderer>().sprite = emojiIcons[i];
			}
		}
	}
}
