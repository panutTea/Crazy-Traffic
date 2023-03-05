using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveStates 
{
	Stop,
	Speeding,
	Moving,
	Braking
}

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
public class Car : MonoBehaviour
{
	[SerializeField]
	public float currentSpeed { get; private set; } // Current speed of the car
	[SerializeField]
	public float currentMaxSpeed { get; private set; }

	private float sensorLength = 5f;
	private float sensorAngle = 30;
	
	public bool isCrash { get; private set; }
	public bool isCrazy;

	private GameObject dollyCart;
	private GameManager gameManager;

	public float accelerationRate = 5f; // Rate at which the car accelerates
	public float brakeRate = 10f; // Rate at which the car brakes
	public int score;
	public float maxSpeed;
	public Lane fromLane;
	public Lane targetLane;
	public LaneStatus laneStatus = LaneStatus.OnFromLane;


	public LayerMask layerMask;
	
	public MoveStates moveState;
	
	private Animator animator;

	//Sound
	public AudioClip crashSound;
	private AudioSource carAudio;
	public bool checkCrash = false;

	// Start is called before the first frame update
	void Start()
	{
		isCrash = false;
		
		animator = GetComponentInChildren<Animator>();
		
		dollyCart = gameObject.transform.parent.gameObject;
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		currentMaxSpeed = maxSpeed;
		currentSpeed = currentMaxSpeed;
		
		moveState = MoveStates.Speeding;

		carAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!isCrash)
		{
			// If car is stop
			if (currentMaxSpeed == 0 && moveState == MoveStates.Braking) 
			{
				Stop();
			}
			
			// If car is moving
			else if (currentMaxSpeed != 0)
			{
				Moving();
			}		
		}
		else
		{
			Stop();
			if (checkCrash == true) 
			{
				carAudio.PlayOneShot(crashSound, 1.0f);
				checkCrash = false;
			}
			
		}
		
		// Debug //
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			if (moveState == MoveStates.Moving) 
			{
				StopCar();
			} else { ReleaseCar(); }
		};
	}
	
	public void ReleaseCar() 
	{
		Debug.Log("ReleaseCar");
		moveState = MoveStates.Speeding;
		
		//Debug
		// _currentMaxSpeed = maxSpeed;
	}
	
	public void StopCar() 
	{
		// Debug.Log("StopCar");
		currentMaxSpeed = 0;
		moveState = MoveStates.Braking;
	}
	
	// Set car to move for moveState and animation
	private void Moving() 
	{
		if (moveState == MoveStates.Speeding) 
		{
			// Debug.Log("Moving");
			animator.SetBool("Moving", true);
			moveState = MoveStates.Moving;
		}
		
		// For speeding up and braking
		if (currentSpeed < currentMaxSpeed)
		{
			// Debug.Log("Speeding");
			SpeedUp();
		}
		else if (currentSpeed > currentMaxSpeed)
		{
			// Debug.Log("Braking");
			Brake();
		}
	}
	
	// Set car to stop for moveState and animation
	private void Stop()
	{
		Debug.Log("Stop");
		animator.SetBool("Moving", false);
		if (currentSpeed != 0)
		{
			Brake();
		}
		else 
		{
			moveState = MoveStates.Stop;
		}
	}
	
	private void SpeedUp()
	{
		currentSpeed = Mathf.Clamp(currentSpeed + accelerationRate * Time.deltaTime, 0, currentMaxSpeed);
		dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = currentSpeed;
	}

	private void Brake()
	{
		currentSpeed = Mathf.Clamp(currentSpeed - brakeRate * Time.deltaTime, 0, maxSpeed);
		dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = currentSpeed;
	}
	
	void FixedUpdate()
	{
		if (isCrazy)
		{
			if (isCrash)
			{
				currentMaxSpeed = 0;
				moveState = MoveStates.Braking;
			}
			else
			{
				currentMaxSpeed = maxSpeed;
				
			}
		}
		else if (!isCrash) 
		{
			CarSensor();
		}
	}

	private void CarSensor()
	{
		RaycastHit hit = new RaycastHit();


		Vector3 sensorStartPos = transform.position + transform.forward * 2f + new Vector3(0, 1, 0);

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
			if (hit.collider.gameObject.GetComponent<Car>().isCrash)
			{
				AdjustTheSpeed(hit, sensorStartPos);
			}
			else if(laneStatus == LaneStatus.OnFromLane)
			{
				if (fromLane == hit.collider.gameObject.GetComponent<Car>().fromLane)
				{
					AdjustTheSpeed(hit, sensorStartPos);
				}
			}
			else if (laneStatus == LaneStatus.OnCenter)
			{
				if (fromLane == hit.collider.gameObject.GetComponent<Car>().fromLane || targetLane == hit.collider.gameObject.GetComponent<Car>().targetLane)
				{
					AdjustTheSpeed(hit, sensorStartPos);
				}
			}
			else if (laneStatus == LaneStatus.OnTargetLane)
			{
				if (targetLane == hit.collider.gameObject.GetComponent<Car>().targetLane)
				{
					AdjustTheSpeed(hit, sensorStartPos);
				}
			}
			

		}
		else
		{
			if (OnResetSpeed && (moveState == MoveStates.Moving || moveState == MoveStates.Speeding))
			{
				currentMaxSpeed = maxSpeed;
			}
			//Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.green);
		}
	}

	private void AdjustTheSpeed(RaycastHit hit, Vector3 sensorStartPos)
	{
		float o_speed = hit.collider.gameObject.transform.GetComponent<Car>().currentMaxSpeed;
		currentMaxSpeed = o_speed > currentMaxSpeed ? currentMaxSpeed : o_speed;
		moveState = MoveStates.Braking;
		//Debug.DrawLine(sensorStartPos, hit.point, Color.red);
		//Debug.Log("Detected car: " + hit.collider.gameObject.tag);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Car" && gameManager.isGameActive && (laneStatus == LaneStatus.OnCenter || collision.gameObject.transform.GetComponent<Car>().laneStatus == LaneStatus.OnCenter))
		{
			Debug.Log(gameObject.name + " Col "+ collision.gameObject.name);
			gameManager.GameOver();
			Crash();
			collision.gameObject.transform.GetComponent<Car>().Crash();
		}
	}


    public void Crash()
	{
		isCrash = true;
		currentMaxSpeed = 0;
		Debug.Log(gameObject.name+" Crash");
		checkCrash = true;
	}

	IEnumerator DelayedGameOver()
	{
		yield return new WaitForSeconds(2.0f);
		dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = 0;

	}

}

