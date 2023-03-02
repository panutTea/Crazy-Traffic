using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveStates 
{
    Stop,
    Moving
};
public class Cars : MonoBehaviour
{
    public int score;
    public float speed;

    
    public MoveStates moveState;

    public bool canControl;

    private void Start() {
        moveState = MoveStates.Moving;
        canControl = true;
    }
}
