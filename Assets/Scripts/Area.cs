using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
	public Lane lane;
	private GameObject car;
	private GameManager gameManager;
	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
   
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Car" && lane == other.gameObject.GetComponent<Car>().targetLane)
		{
			car = other.gameObject;
			other.gameObject.GetComponent<Car>().laneStatus = LaneStatus.OnTargetLane;
			if (!gameManager.isGameOver)
			{
				// scoreToAdd is the car's score, which will change depending on the car's layout
				int scoreToAdd = car.GetComponent<Car>().score;
				gameManager.UpdateScore(scoreToAdd);
			}
			// Spawn special item
			other.GetComponent<CarSpecialItem>().DropSpecialItem();
			//Debug.Log(other.gameObject.transform.name + " Form "+other.gameObject.GetComponent<CarSensor>().fromLane + " to "+other.gameObject.GetComponent<CarSensor>().targetLane + " status " + other.gameObject.GetComponent<CarSensor>().laneStatus);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "Car" && lane == other.gameObject.GetComponent<Car>().fromLane)
		{
			other.gameObject.GetComponent<Car>().laneStatus = LaneStatus.OnCenter;
			//Debug.Log(other.gameObject.transform.name + " Form "+other.gameObject.GetComponent<CarSensor>().fromLane + " to "+other.gameObject.GetComponent<CarSensor>().targetLane + " status " + other.gameObject.GetComponent<CarSensor>().laneStatus);
		}
	}
}
