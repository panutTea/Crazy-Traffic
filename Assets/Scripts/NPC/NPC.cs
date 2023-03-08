using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
	private Camera cam;
	
	private NavMeshAgent agent;
	
	private Animator animator;
	
	public GameObject destination;
	
	[SerializeField] private float speed = 0;
	
	[Header("Sensor")]
	[SerializeField] private float sensorLength = 3;
	[SerializeField] private float sensorRange = 0.5f;
	public LayerMask layerMask;
	
	[Header("Change Move")]
	[SerializeField] private float probMovingToIdle = 0.2f;
	[SerializeField] private float probIdleToMoving = 0.5f;
	[SerializeField] private float probIdleToIdle = 0.2f;
	[SerializeField] private float changeMoveTime = 5; // in seconds
	private bool isIdle;
	
	private void Start()
	{
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		
		agent.speed = speed;
		agent.SetDestination(destination.transform.position);
		
		// Start to random to change a move
		StartCoroutine(TimeToChangeIdle());
	}

	// Update is called once per frame
	void Update()
	{
		// Right sensor
		bool isRightHit1 = IsSensorHit(sensorRange);
		bool isRightHit2 = IsSensorHit(1.5f * sensorRange);
		// Left sensor
		bool isLeftHit1 = IsSensorHit(-sensorRange);
		bool isLeftHit2 = IsSensorHit(1.5f * -sensorRange);
		
		// If any other npc stay at the forward right, move left
		if (!isIdle) 
		{
			if (isRightHit1 || isRightHit2) 
			{
				// Debug.Log(gameObject.name + ": NPC ahead on the right!!");
				transform.Translate(Vector3.left * Time.deltaTime);
			}
			// If any other npc stay at the forward left, move right
			else if (isLeftHit1 || isLeftHit2)
			{
				// Debug.Log(gameObject.name + ": NPC ahead on the left!!");
				transform.Translate(Vector3.right * Time.deltaTime);
			}
		}
		
		SetSpeed();
	}
	
	void SetSpeed() 
	{
		// Debug.Log(agent.velocity.magnitude);
		animator.SetFloat("Speed_f", agent.velocity.magnitude);
	}
	
	// Is sensor hit an npc?
	bool IsSensorHit(float sensorRange) 
	{
		RaycastHit hit = new RaycastHit();
		
		Vector3 sensorStartPos = transform.position + (transform.forward * 0.2f) + (transform.right * sensorRange) + new Vector3(0, 0.2f, 0);
		Vector3 sensorDirection = Quaternion.AngleAxis(0, transform.up) * transform.forward;
		
		if (Physics.Raycast(sensorStartPos, sensorDirection, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag == "NPC")
		{
			Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.red);
			return true;
		}
		else 
		{
			Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.green);
			return false;
		}
	}
	
	IEnumerator TimeToChangeIdle() 
	{
		yield return new WaitForSeconds(changeMoveTime);
		
		if (isIdle) 
		{
			RandomToMove();
			RandomToChangeIdle();
		}
		else 
		{
			RandomToIdle();
		}
		
		StartCoroutine(TimeToChangeIdle());
	}
	
	// Change animation from Moving (Walking, Running) -> Idle
	void RandomToIdle() 
	{
		// Debug.Log("ChangeToIdle");
		float randomProb = Random.Range(0.0f, 1.0f);
		if (randomProb < probMovingToIdle) 
		{
			agent.SetDestination(transform.position);
			isIdle = true;
		}
	}
	
	void RandomToMove() 
	{
		// Debug.Log("ChangeToMove");
		float randomProb = Random.Range(0.0f, 1.0f);
		if (randomProb < probIdleToMoving) 
		{
			agent.SetDestination(destination.transform.position);
			isIdle = false;
		}
	}
	
	// Change idle move animation from Idle1 -> Idle2
	void RandomToChangeIdle() 
	{
		// Debug.Log("ChangeIdle");
		float randomProb = Random.Range(0.0f, 1.0f);
		if (randomProb < probIdleToIdle) 
		{
			animator.SetTrigger("ChangeIdle");
		}
	}
}
