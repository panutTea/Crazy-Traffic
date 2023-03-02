using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : MonoBehaviour
{
	public Color outlineColor = Color.black;
	
	[Header("Bouncing")]
	public float amplitude = 0.5f;
	public float frequency = 1f;
	public float bottomBound;
	public float getDownSpeed;

	// Position Storage Variables
	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();
	
	private bool isStart = false;
	private float time;

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y > bottomBound && !isStart) 
		{
			Debug.Log("not start");
			transform.Translate(Vector3.down * getDownSpeed * Time.deltaTime);
			posOffset = transform.position;
		} 
		else 
		{	
			if (!isStart) 
			{
				isStart = true;
			}

			tempPos = posOffset;
			tempPos.y += Mathf.Sin(time * Mathf.PI * frequency) * amplitude;
			time += Time.deltaTime;
			transform.position = tempPos;
		}
	}
}
