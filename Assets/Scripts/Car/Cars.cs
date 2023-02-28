using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    public int score;
    public float speed;

    public enum MoveStates {Stop, Moving};
    public MoveStates moveState;

    public enum Lanes {Straight, Left, Right};
    public Lanes lane;

    public bool onLane;

    public bool canControl;

    private void Start() {
        moveState = MoveStates.Moving;
        onLane = true;
        canControl = true;
    }
}
