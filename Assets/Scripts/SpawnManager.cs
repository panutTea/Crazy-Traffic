using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] carPrefabs;
    public GameObject[] pathPrefabs;

    private float startDelay = 1.5f;
    private float repeatRate = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomPath", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomPath()
    {
        int pathIndex = Random.Range(0, pathPrefabs.Length);
        int carIndex = Random.Range(0, carPrefabs.Length);
        Vector3 spawnPathPos = new Vector3(0, 0, 0);
        GameObject currentCar = carPrefabs[carIndex];
        GameObject currentPath = Instantiate(pathPrefabs[pathIndex], spawnPathPos, pathPrefabs[pathIndex].transform.rotation);
        GameObject dollyCart = currentPath.transform.GetChild(1).gameObject;
        dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = currentCar.GetComponent<Cars>().speed;
        currentCar = Instantiate(currentCar, dollyCart.transform.position, dollyCart.transform.rotation);


        currentCar.transform.SetParent(dollyCart.transform);

    }

}
