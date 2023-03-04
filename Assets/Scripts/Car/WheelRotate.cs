using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    private GameObject parent;
    private Cars carScript;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        carScript = parent.GetComponent<Cars>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carScript.moveState == Cars.MoveStates.Moving) {
            transform.Rotate(Vector3.right * carScript.currentSpeed * Time.deltaTime);
        }
    }
}
