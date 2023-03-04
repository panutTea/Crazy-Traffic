using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPath : MonoBehaviour
{

    public float destination;
    private GameObject dollyCart;
    private GameObject car;
    private GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        dollyCart = gameObject.transform.GetChild(1).gameObject;
        car = dollyCart.transform.GetChild(0).gameObject;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position > destination)
        {
            if (!gameManager.isGameOver)
            {
                // scoreToAdd is the car's score, which will change depending on the car's layout
                int scoreToAdd = car.GetComponent<Car>().score;
                gameManager.UpdateScore(scoreToAdd);
            }
            Destroy(gameObject);
        }
    }
}
