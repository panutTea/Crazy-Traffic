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
      
    }

    private void setStopCars()
    {
        foreach (var car in cars)
        {
            car.transform.GetComponent<Car>().StopCar();
        }
    }
}
