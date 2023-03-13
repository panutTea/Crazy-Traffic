using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveStates 
{
	Stop,
	// Speeding,
	Moving,
	// Braking
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
	public float currentSpeed { get; private set; } // Current speed of the car
	public float currentMaxSpeed { get; private set; }

	private float sensorLength = 5f;
	private float sensorAngle = 30;
	private bool isForcedStop = false;
	public bool isCrash { get; private set; }
	public bool isCrazy;

	private GameObject dollyCart;
	private GameManager gameManager;
	private GameObject spwanManager;


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

	//Effect
	public ParticleSystem explosionParticle;

	// Start is called before the first frame update
	void Start()
	{
		isCrash = false;
		animator = GetComponentInChildren<Animator>();
		
		dollyCart = gameObject.transform.parent.gameObject;
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		carAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
	{
        
		if (currentSpeed == 0 && moveState == MoveStates.Moving) 
		{
			moveState = MoveStates.Stop;
			animator.SetBool("Moving", false);
		}
		else if (currentSpeed != 0 && moveState == MoveStates.Stop)
		{
			moveState = MoveStates.Moving;
			animator.SetBool("Moving", true);
		}
		
		if (!isCrash)
		{
			// If car is stop
			if (currentMaxSpeed == 0 && currentSpeed != 0) 
			{
				Stop();
			}
			
			// If car is moving
			else if (currentMaxSpeed != 0 && currentSpeed != currentMaxSpeed)
			{
				Moving();
			}
		}
		else if (moveState != MoveStates.Stop)
		{
			Stop();
		}
		
		
		// Debug try to controll //
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			if (moveState == MoveStates.Moving) 
			{
				StopCar();
			} else { ReleaseCar(); }
		};
	}

	void FixedUpdate()
	{
		if (isCrazy && !isCrash)
		{
			currentMaxSpeed = maxSpeed;
		}
		else if (!isCrash)
		{
			CarSensor();
		}
		
	}
	public void setDefaultSpeed()
    {
		currentMaxSpeed = maxSpeed;
		currentSpeed = currentMaxSpeed;
	}
	public void ReleaseCar() 
	{
		Debug.Log("Release");
		if (!isCrash)
        {
			isForcedStop = false;
			currentMaxSpeed = maxSpeed;
		}
		
        if (!gameManager.isGameActive)
        {
			gameManager.SetGameAvtive();
        }
	}
	
	public void StopCar() 
	{
        if (laneStatus == LaneStatus.OnFromLane && !isCrazy) {
			Debug.Log("Stop!!");
			isForcedStop = true;
			currentMaxSpeed = 0;
		}
	}
		
	
	// Set car to move for moveState and animation
	private void Moving() 
	{
		// For speeding up and braking
		if (currentSpeed < currentMaxSpeed)
		{
			SpeedUp();
		}
		else if (currentSpeed > currentMaxSpeed)
		{
			Brake();
		}
	}
	
	// Set car to stop for moveState and animation
	private void Stop()
	{
		if (currentSpeed != 0)
		{
			Brake();
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

	private void RaySensor(RaycastHit hit, Vector3 sensorStartPos, Vector3 sensorDirection, float sensorAngle, bool isCenterRay = false)
	{
		sensorDirection = Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward;
		if (Physics.Raycast(sensorStartPos, sensorDirection, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag == "Car")
		{
			if (hit.collider.gameObject.GetComponent<Car>().isCrash && isCenterRay)
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
				if (fromLane == hit.collider.gameObject.GetComponent<Car>().fromLane || (isCenterRay && targetLane == hit.collider.gameObject.GetComponent<Car>().targetLane))
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
			if (isCenterRay && !isForcedStop)
			{
				currentMaxSpeed = maxSpeed;
			}
			//Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.green);
		}
	}

	private void AdjustTheSpeed(RaycastHit hit, Vector3 sensorStartPos)
	{
		float o_speed = hit.collider.gameObject.transform.GetComponent<Car>().currentSpeed;
		
		if ( o_speed < currentMaxSpeed )
		{
			currentMaxSpeed = o_speed;
			//Debug.Log(gameObject.name + " "+ hit.collider.gameObject.name+" Detect to Braking");
		}
		Debug.DrawLine(sensorStartPos, hit.point, Color.red);
		//Debug.Log("Detected car: " + hit.collider.gameObject.tag);
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Car" && !gameManager.isGameOver && (laneStatus == LaneStatus.OnCenter || collision.gameObject.transform.GetComponent<Car>().laneStatus == LaneStatus.OnCenter))
		{
			Debug.Log(gameObject.name + " Col "+ collision.gameObject.name);
			gameManager.GameOver();
			Crash();
			collision.gameObject.transform.GetComponent<Car>().Crash();
			explosionParticle.Play();
		}
	}

	public void Crash()
	{
		isCrash = true;
		currentMaxSpeed = 0;
		Debug.Log(gameObject.name+" Crash");
	}


}

