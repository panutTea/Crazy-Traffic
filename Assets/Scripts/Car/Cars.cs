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
	public int score { get; }
	public float speed { get; }
	
	[SerializeField]
	public MoveStates moveState { get; private set; }

	// Start is called before the first frame update
	void Start()
	{
		moveState = MoveStates.Moving;
	}
	
	private void Update()
	{
		
	}
	
	public void Moving() 
	{
		moveState = MoveStates.Moving;
	}
	
	public void Stop() 
	{
		moveState = MoveStates.Stop;
	}
}

