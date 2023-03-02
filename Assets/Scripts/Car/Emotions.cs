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
	public Text emoStateText;

	private float emoValues;

	// A value to increase emotion value per second
	public float emoIncreasingValue = 5;
	// A value to decrease emotion value per second
	public float emoDecreasingValue = -10;
	public float EMO_MAXIUM = 100;

	// State of emotion (5 states)
	public float[] emoStateValueList = {0, 0.25f, 0.5f, 0.75f, 1};

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
				emoStateText.text = ((EmoStates) i).ToString();
			}
		}
		// if (emoValues >= emoStateValueList[4] * EMO_MAXIUM) {
		//     emoState = EmoStates.Furious;
		//     emoStateText.text = "Furious";
		//     carScript.moveState = MoveStates.Moving;
		//     carScript.canControl = false;
		// }
		// else if (emoValues >= emoStateValueList[3] * EMO_MAXIUM) {
		//     emoState = EmoStates.Anger;
		//     emoStateText.text = "Anger";
		// }
		// else if (emoValues >= emoStateValueList[2] * EMO_MAXIUM) {
		//     emoState = EmoStates.Fine;
		//     emoStateText.text = "Fine";
		// }
		// else if (emoValues >= emoStateValueList[1] * EMO_MAXIUM) {
		//     emoState = EmoStates.Good;
		//     emoStateText.text = "Good";
		// }
		// else if (emoValues >= emoStateValueList[0] * EMO_MAXIUM) {
		//     emoState = EmoStates.Happy;
		//     emoStateText.text = "Happy";
		// }
	}
}
