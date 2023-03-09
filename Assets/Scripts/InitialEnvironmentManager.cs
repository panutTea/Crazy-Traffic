using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialEnvironmentManager : MonoBehaviour
{
    public GameObject[] cars;
    // Start is called before the first frame update
    void Start()
    {
        setStopCars();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cars[0].transform.GetComponent<Car>().ReleaseCar();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            cars[1].transform.GetComponent<Car>().ReleaseCar();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            cars[2].transform.GetComponent<Car>().ReleaseCar();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            cars[0].transform.GetComponent<Car>().StopCar();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cars[1].transform.GetComponent<Car>().StopCar();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            cars[2].transform.GetComponent<Car>().StopCar();
        }
    }

    private void setStopCars()
    {
        foreach (var car in cars)
        {
            car.transform.GetComponent<Car>().StopCar();
        }
    }
}
