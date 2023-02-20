using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emotions : MonoBehaviour
{
    public Slider emoBar;

    private float emoValues;

    // A value to increase emotion value per second
    public float emoIncreasingValue = 5;
    public float EMO_MAXIUM = 100;

    // State of emotion (5 states)
    public enum EmoStates {Happy, Good, Fine,  Anger, Furious};
    public float[] emoStateValueList = {0, 0.25f, 0.5f, 0.75f, 1};

    public EmoStates emoState = EmoStates.Happy; 

    // Start is called before the first frame update
    void Start()
    {
        // Set maximum value of emotion bar
        emoBar.maxValue = EMO_MAXIUM;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEmotionValues();
    }

    void UpdateEmotionValues() {
        emoValues += emoIncreasingValue * Time.deltaTime;
        emoBar.value = emoValues;

        // Change state
        if (emoValues >= emoStateValueList[4] * EMO_MAXIUM) {
            emoState = EmoStates.Furious;
            Debug.Log("Furious");
        }
        else if (emoValues >= emoStateValueList[3] * EMO_MAXIUM) {
            emoState = EmoStates.Anger;
            Debug.Log("Anger");
        }
        else if (emoValues >= emoStateValueList[2] * EMO_MAXIUM) {
            emoState = EmoStates.Fine;
            Debug.Log("Fine");
        }
        else if (emoValues >= emoStateValueList[1] * EMO_MAXIUM) {
            emoState = EmoStates.Good;
            Debug.Log("Good");
        }
        else if (emoValues >= emoStateValueList[0] * EMO_MAXIUM) {
            emoState = EmoStates.Happy;
            Debug.Log("Happy");
        }
    }
}
