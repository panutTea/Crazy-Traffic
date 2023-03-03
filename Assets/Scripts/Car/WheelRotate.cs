using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
	private Cars carScript;
	// Start is called before the first frame update
	void Start()
	{
		carScript = GetComponentInParent<Cars>();
	}

	// Update is called once per frame
	void Update()
	{
		if (carScript.moveState == MoveStates.Moving) {
			transform.Rotate(Vector3.right * carScript.speed * Time.deltaTime);
		}
	}
}
