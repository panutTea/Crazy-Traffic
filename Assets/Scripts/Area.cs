using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Lane lane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Car" && lane == other.gameObject.GetComponent<Cars>().targetLane)
        {
            other.gameObject.GetComponent<Cars>().laneStatus = LaneStatus.OnTargetLane;
            //Debug.Log(other.gameObject.transform.name + " Form "+other.gameObject.GetComponent<Cars>().fromLane + " to "+other.gameObject.GetComponent<Cars>().targetLane + " status " + other.gameObject.GetComponent<Cars>().laneStatus);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Car" && lane == other.gameObject.GetComponent<Cars>().fromLane)
        {
            other.gameObject.GetComponent<Cars>().laneStatus = LaneStatus.OnCenter;
            //Debug.Log(other.gameObject.transform.name + " Form "+other.gameObject.GetComponent<Cars>().fromLane + " to "+other.gameObject.GetComponent<Cars>().targetLane + " status " + other.gameObject.GetComponent<Cars>().laneStatus);
        }
    }
}
