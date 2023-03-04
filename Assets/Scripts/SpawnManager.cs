using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    public GameObject[] carPrefabs;
    public GameObject[] pathPrefabs;

    private float startDelay = 1.5f;
    private float repeatRate = 1.5f;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnRandomPath", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomPath()
    {
        if (gameManager.isGameActive)
        {
            //int pathIndex = Random.Range(0, 3);
            int pathIndex = Random.Range(0, pathPrefabs.Length);
            Lane lane = (Lane)(pathIndex/2);

            int carIndex = Random.Range(0, carPrefabs.Length);
            Vector3 spawnPathPos = new Vector3(0, 0, 0);
            GameObject currentCar = carPrefabs[carIndex];
            GameObject currentPath = Instantiate(pathPrefabs[pathIndex], spawnPathPos, pathPrefabs[pathIndex].transform.rotation);
            GameObject dollyCart = currentPath.transform.GetChild(1).gameObject;
            dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = currentCar.GetComponent<Car>().maxSpeed;
            currentCar.GetComponent<Car>().fromLane = lane;
            currentCar.GetComponent<Car>().targetLane = GetLaneTarget(pathIndex);
            //Debug.Log("Spawn "+currentCar.transform.name + " Form "+currentCar.GetComponent<Cars>().fromLane+" To "+currentCar.GetComponent<Cars>().targetLane);
            currentCar = Instantiate(currentCar, dollyCart.transform.position, dollyCart.transform.rotation);


            currentCar.transform.SetParent(dollyCart.transform);
        }
        
    }

    private Lane GetLaneTarget(int pathIndex)
    {
        if (pathIndex == 2 || pathIndex == 4)
            return Lane.Left;
        else if (pathIndex == 0 || pathIndex == 5)
            return Lane.Right;
        else
            return Lane.Top;
    }

}
