using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    public GameObject[] carPrefabs;
    public GameObject[] pathPrefabs;

    private float startDelay = 1.5f;
    private float repeatRate = 1.5f;
    private int spawnAmbulance = 0;
    private int spawnLevel = 0;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnAmbulance = 0;
        spawnLevel = 1;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnRandomPath", startDelay, repeatRate);
        Debug.Log(gameManager.level);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Game level :"+gameManager.level);
        Debug.Log("Spawn Level :"+spawnLevel);
        if (spawnLevel +1  == gameManager.level)
        {
            spawnAmbulance += spawnLevel * 2;
            spawnLevel++;
        }
    }

    void SpawnRandomPath()
    {
        if (gameManager.isGameActive)
        {
            //int pathIndex = Random.Range(0, 3);
            int pathIndex = Random.Range(0, pathPrefabs.Length);
            Lane lane = (Lane)(pathIndex/2);

            int carIndex = Random.Range(0, NumCarsUsedRandomSpawn());
            if (carIndex == 3)
            {
                Debug.Log("Spawn LV: "+spawnLevel+ " Ambulance: "+ spawnAmbulance + " to "+ (spawnAmbulance-1));
                spawnAmbulance--;
            }
            Vector3 spawnPathPos = new Vector3(0, 0, 0);
            GameObject currentCar = carPrefabs[carIndex];
            GameObject currentPath = Instantiate(pathPrefabs[pathIndex], spawnPathPos, pathPrefabs[pathIndex].transform.rotation);
            GameObject dollyCart = currentPath.transform.GetChild(1).gameObject;
            dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = currentCar.GetComponent<Car>().maxSpeed;
            
            currentCar.GetComponent<Car>().fromLane = lane;
            currentCar.GetComponent<Car>().targetLane = GetLaneTarget(pathIndex);
            //Debug.Log("Spawn "+currentCar.transform.name + " Form "+currentCar.GetComponent<Cars>().fromLane+" To "+currentCar.GetComponent<Cars>().targetLane);
            currentCar = Instantiate(currentCar, dollyCart.transform.position, dollyCart.transform.rotation);

            currentCar.GetComponent<Car>().setDefaultSpeed();
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

    private int NumCarsUsedRandomSpawn()
    {
        
        if (spawnLevel > 1)
        {
            if (spawnAmbulance > 0)
            {
                return 4;
            }
            return 3;
        }
        return 2;
    }

}
