using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    public int score;
    public float speed;

    public enum MoveStates {Stop, Moving};
    public MoveStates moveState = MoveStates.Stop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

