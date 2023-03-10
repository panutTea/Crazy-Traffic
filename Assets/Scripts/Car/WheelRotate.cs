using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
	private Car carScript;
	// Start is called before the first frame update
	void Start()
	{
		carScript = GetComponentInParent<Car>();
	}

    // Update is called once per frame
    void Update()
    {
        if (carScript.moveState == MoveStates.Moving) {
            transform.Rotate(Vector3.right * carScript.currentSpeed * Time.deltaTime);
        }
    }
}
