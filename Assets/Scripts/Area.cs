using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
	public Lane lane;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
   
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Car" && lane == other.gameObject.GetComponent<CarSensor>().targetLane)
		{
			other.gameObject.GetComponent<CarSensor>().laneStatus = LaneStatus.OnTargetLane;
			
			// Spawn special item
			other.GetComponent<CarSpecialItem>().DropSpecialItem();
			//Debug.Log(other.gameObject.transform.name + " Form "+other.gameObject.GetComponent<CarSensor>().fromLane + " to "+other.gameObject.GetComponent<CarSensor>().targetLane + " status " + other.gameObject.GetComponent<CarSensor>().laneStatus);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "Car" && lane == other.gameObject.GetComponent<CarSensor>().fromLane)
		{
			other.gameObject.GetComponent<CarSensor>().laneStatus = LaneStatus.OnCenter;
			//Debug.Log(other.gameObject.transform.name + " Form "+other.gameObject.GetComponent<CarSensor>().fromLane + " to "+other.gameObject.GetComponent<CarSensor>().targetLane + " status " + other.gameObject.GetComponent<CarSensor>().laneStatus);
		}
	}
}
