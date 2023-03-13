using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : MonoBehaviour
{
	public Color outlineColor = Color.black;
	
	private GameObject player;
	
	private Rigidbody _rigidbody;
	
	private GameObject mainCam;
	[SerializeField] private bool isShowOnly = false;
	[SerializeField] private float moveSpeed = 5;
	private bool isStartToMove = false;
	
	[SerializeField] private float onTheGroundTime = 2;
	
	[Header("Bouncing")]
	public float amplitude = 0.5f;
	public float frequency = 1f;
	public float bottomBound;
	public float getDownSpeed;

	// Position Storage Variables
	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();
	
	[Header("Rotate")]
	[SerializeField] private float rotateSpeed = 5;
	
	private bool isStartToBounce = false;
	private float time;
	
	[SerializeField] private float rangeToDestroy = 2;
	
	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		_rigidbody = GetComponent<Rigidbody>();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		if (!isShowOnly)
		{
			StartCoroutine(GotoPlayer());
		}
	}

	// Update is called once per frame
	void Update()
	{
		Rotating();
		
		if (isStartToMove)
		{
			Vector3 towardCamera = mainCam.transform.position + Vector3.down - transform.position;
			_rigidbody.AddForce(towardCamera * Time.deltaTime * moveSpeed, ForceMode.Impulse);
			Debug.Log(towardCamera.magnitude + " " + rangeToDestroy);
			
			if (towardCamera.magnitude < rangeToDestroy)
			{
				player.GetComponent<PlayerSpecialUsing>().AddHallsCool();
				Destroy(gameObject);
			}
		}
		else if (!isShowOnly)
		{
			Bouncing();
		}
	}
	
	private void Bouncing() 
	{
		if (transform.position.y > bottomBound && !isStartToBounce) 
		{
			transform.Translate(Vector3.down * getDownSpeed * Time.deltaTime);
			posOffset = transform.position;
		} 
		else 
		{	
			if (!isStartToBounce) 
			{
				isStartToBounce = true;
			}

			tempPos = posOffset;
			tempPos.y += Mathf.Sin(time * Mathf.PI * frequency) * amplitude;
			time += Time.deltaTime;
			transform.position = tempPos;
		}
	}
	
	private void Rotating() 
	{
		transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
	}
	
	IEnumerator GotoPlayer() 
	{
		yield return new WaitForSeconds(onTheGroundTime);
		isStartToMove = true;
	}
}
