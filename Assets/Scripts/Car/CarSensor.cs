using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSensor : MonoBehaviour
{

    private GameObject car;
    private GameObject dollyCart;

    // Start is called before the first frame update
    void Start()
    {
        car = gameObject.transform.parent.gameObject;
        dollyCart = car.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car" && other.gameObject != car)
        {
            Debug.Log(car.name+" DETECT "+other.gameObject.name);
            float o_speed = other.transform.parent.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
            float c_speed = dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
            dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = o_speed > c_speed? c_speed: o_speed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car" )
        {
            //Debug.Log("NONE");
            //dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = car.GetComponent<Cars>().speed;
        }
    }
    /**
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("DETECT");
            dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("NONE");
            dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = car.GetComponent<Cars>().speed;
        }
    }
    **/
}
