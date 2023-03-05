using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPath : MonoBehaviour
{

    public float destination;
    private GameObject dollyCart;
    
    

    // Start is called before the first frame update
    void Start()
    {
        dollyCart = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position > destination)
        {
            Destroy(gameObject);
        }
    }
}
