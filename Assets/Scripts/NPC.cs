using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
	private Camera cam;
	
	private NavMeshAgent agent;
	
	private Animator animator;
	
	[SerializeField]
	private float speed = 0;
	
	[SerializeField]
	private float probIdle = 0.15f;
	
	private void Start()
	{
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		
		StartCoroutine(TimeToChangeIdle());
	}

	// Update is called once per frame
	void Update()
	{
		SetSpeed();
		
		// Test with click to select destination on scene.
		if (Input.GetMouseButton(0)) 
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit)) 
			{
				agent.SetDestination(hit.point);
				
			}
		}
	}
	
	void SetSpeed() 
	{
		Debug.Log(agent.velocity.magnitude);
		agent.speed = speed;
		animator.SetFloat("Speed_f", agent.velocity.magnitude);
	}
	
	IEnumerator TimeToChangeIdle() 
	{
		Debug.Log("ChangeIdle");
		yield return new WaitForSeconds(5);
		RandomToChangeIdle();
		StartCoroutine(TimeToChangeIdle());
	}
	
	void RandomToChangeIdle() 
	{
		float randomProb = Random.Range(0.0f, 1.0f);
		if (probIdle < randomProb) 
		{
			animator.SetTrigger("ChangeIdle");
		}
	}
}
