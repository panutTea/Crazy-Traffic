using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane
{
	Left,
	Right,
	Top
}

public enum LaneStatus
{
	OnFromLane,
	OnCenter,
	OnTargetLane
}

public class CarSensor : MonoBehaviour
{
	public Lane fromLane;
	public Lane targetLane;
	public LaneStatus laneStatus = LaneStatus.OnFromLane;
	
	public LayerMask layerMask;

	private float sensorLength = 5f;
	private float sensorAngle = 30;


	private GameObject dollyCart;
	
	// Start is called before the first frame update
	void Start()
	{
		dollyCart = gameObject.transform.parent.gameObject;
	}

	void FixedUpdate()
	{
		RaycastHit hit = new RaycastHit();

		
		Vector3 sensorStartPos = transform.position + transform.forward * 2f + new Vector3(0,1,0);

		Vector3 sensorDirection = new Vector3();

		// Cast a ray in front of the car
		RaySensor(hit, sensorStartPos, sensorDirection, 0, true);
		// Cast a ray to the right of the car
		RaySensor(hit, sensorStartPos, sensorDirection, sensorAngle);
		RaySensor(hit, sensorStartPos, sensorDirection, 2 * sensorAngle);
		// Cast a ray to the left of the car
		RaySensor(hit, sensorStartPos, sensorDirection, -sensorAngle);
		RaySensor(hit, sensorStartPos, sensorDirection, -2 * sensorAngle);
	}

	private void RaySensor(RaycastHit hit, Vector3 sensorStartPos, Vector3 sensorDirection, float sensorAngle, bool OnResetSpeed = false)
	{
		sensorDirection = Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward;
		if (Physics.Raycast(sensorStartPos, sensorDirection, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag == "Car")
		{
			if (laneStatus == LaneStatus.OnFromLane)
			{
				if (fromLane == hit.collider.gameObject.GetComponent<CarSensor>().fromLane)
				{
					float o_speed = hit.collider.gameObject.transform.parent.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
					float c_speed = dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
					dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = o_speed > c_speed ? c_speed : o_speed;
					//Debug.DrawLine(sensorStartPos, hit.point, Color.red);
					//Debug.Log("Detected car: " + hit.collider.gameObject.tag);
				}
			}
			else if (laneStatus == LaneStatus.OnCenter)
			{
				if (fromLane == hit.collider.gameObject.GetComponent<CarSensor>().fromLane || targetLane == hit.collider.gameObject.GetComponent<CarSensor>().targetLane)
				{
					float o_speed = hit.collider.gameObject.transform.parent.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
					float c_speed = dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
					dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = o_speed > c_speed ? c_speed : o_speed;
					//Debug.DrawLine(sensorStartPos, hit.point, Color.red);
					//Debug.Log("Detected car: " + hit.collider.gameObject.tag);
				}
			}
			else if (laneStatus == LaneStatus.OnTargetLane)
			{
				if (targetLane == hit.collider.gameObject.GetComponent<CarSensor>().targetLane)
				{
					float o_speed = hit.collider.gameObject.transform.parent.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
					float c_speed = dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
					dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = o_speed > c_speed ? c_speed : o_speed;
					//Debug.DrawLine(sensorStartPos, hit.point, Color.red);
					//Debug.Log("Detected car: " + hit.collider.gameObject.tag);
				}
			}
			

		}
		else
		{
			if (OnResetSpeed) 
			{
				Cars car = GetComponent<Cars>();				
				dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = car.speed;
			}
			//Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.green);
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Car")
		{
			Debug.Log(gameObject.name + " Col "+ collision.gameObject.name);
		}
	}
}
