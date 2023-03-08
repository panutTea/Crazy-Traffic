using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDestination : MonoBehaviour
{
	private Car carScript;
	public GameObject[] allObjLane;
	private GameObject destLane;

	private void Start() {
		carScript = GetComponentInParent<Car>();
		
		foreach (GameObject lane in allObjLane) 
		{
			if (lane.GetComponent<LaneScript>().lane == carScript.targetLane) 
			{
				destLane = lane;
				break;
			}
		}
	}

	void Update()
	{
		Vector3 v = destLane.transform.position - transform.position;
		v.x = v.z = 0.0f;
		transform.LookAt(destLane.transform.position - v);
		transform.Rotate(0, 180, 0);
	}
}
