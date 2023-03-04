using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane
{
    Left,
    Right,
    Top
}

public enum LaneStatus
{
    OnFromLane,
    OnCenter,
    OnTargetLane
}
public class Cars : MonoBehaviour
{

    private float _currentSpeed = 0f; // Current speed of the car
    private float _currentMaxSpeed;

    private float sensorLength = 5f;
    private float sensorAngle = 30;
    private bool active = true;

    private GameObject dollyCart;
    private GameManager gameManager;

    public float currentSpeed
    {
        get { return _currentSpeed; }
    }
    public float currentMaxSpeed
    {
        get { return _currentMaxSpeed; }
    }

    public float accelerationRate = 5f; // Rate at which the car accelerates
    public float brakeRate = 10f; // Rate at which the car brakes
    public int score;
    public float maxSpeed;
    public Lane fromLane;
    public Lane targetLane;
    public LaneStatus laneStatus = LaneStatus.OnFromLane;
   
    public enum MoveStates {Stop, Moving};
    public MoveStates moveState = MoveStates.Stop;

    public LayerMask layerMask;

    

    // Start is called before the first frame update
    void Start()
    {
        dollyCart = gameObject.transform.parent.gameObject;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _currentMaxSpeed = maxSpeed;
        _currentSpeed = _currentMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            if (currentSpeed != 0)
            {
                Brake();
            }
        }
        else if (active)
        {
            if (currentMaxSpeed != currentSpeed)
            {
               if (currentMaxSpeed > currentSpeed)
                {
                    SpeedUp();
                }
               else
                {
                    Brake();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (active) 
        {
            CarSensor();
        }
    }

    private void CarSensor()
    {
        RaycastHit hit = new RaycastHit();


        Vector3 sensorStartPos = transform.position + transform.forward * 2f + new Vector3(0, 1, 0);

        Vector3 sensorDirection = new Vector3();

        // Cast a ray in front of the car
        RaySensor(hit, sensorStartPos, sensorDirection, 0, true);
        // Cast a ray to the right of the car
        RaySensor(hit, sensorStartPos, sensorDirection, sensorAngle);
        RaySensor(hit, sensorStartPos, sensorDirection, 2 * sensorAngle);
        // Cast a ray to the left of the car
        RaySensor(hit, sensorStartPos, sensorDirection, -sensorAngle);
        RaySensor(hit, sensorStartPos, sensorDirection, -2 * sensorAngle);
    }

    private void RaySensor(RaycastHit hit, Vector3 sensorStartPos, Vector3 sensorDirection, float sensorAngle, bool OnResetSpeed = false)
    {
        sensorDirection = Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward;
        if (Physics.Raycast(sensorStartPos, sensorDirection, out hit, sensorLength, layerMask) && hit.collider.gameObject.tag == "Car")
        {
            if (!hit.collider.gameObject.GetComponent<Cars>().active)
            {
                AdjustTheSpeed(hit, sensorStartPos);
            }
            else if(laneStatus == LaneStatus.OnFromLane)
            {
                if (fromLane == hit.collider.gameObject.GetComponent<Cars>().fromLane)
                {
                    AdjustTheSpeed(hit, sensorStartPos);
                }
            }
            else if (laneStatus == LaneStatus.OnCenter)
            {
                if (fromLane == hit.collider.gameObject.GetComponent<Cars>().fromLane || targetLane == hit.collider.gameObject.GetComponent<Cars>().targetLane)
                {
                    AdjustTheSpeed(hit, sensorStartPos);
                }
            }
            else if (laneStatus == LaneStatus.OnTargetLane)
            {
                if (targetLane == hit.collider.gameObject.GetComponent<Cars>().targetLane)
                {
                    AdjustTheSpeed(hit, sensorStartPos);
                }
            }
            

        }
        else
        {
            if (OnResetSpeed)
                _currentMaxSpeed = maxSpeed;
            //Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.green);
        }
    }

    private void AdjustTheSpeed(RaycastHit hit, Vector3 sensorStartPos)
    {
        float o_speed = hit.collider.gameObject.transform.GetComponent<Cars>().currentMaxSpeed;
        _currentMaxSpeed = o_speed > _currentMaxSpeed ? _currentMaxSpeed : o_speed;
        //Debug.DrawLine(sensorStartPos, hit.point, Color.red);
        //Debug.Log("Detected car: " + hit.collider.gameObject.tag);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car" && gameManager.isGameActive && (laneStatus == LaneStatus.OnCenter || collision.gameObject.transform.GetComponent<Cars>().laneStatus == LaneStatus.OnCenter))
        {
            Debug.Log(gameObject.name + " Col "+ collision.gameObject.name);
            gameManager.GameOver();
            Crash();
            collision.gameObject.transform.GetComponent<Cars>().Crash();
        }
    }

    public void Crash()
    {
        active = false;
        _currentMaxSpeed = 0;
        Debug.Log(gameObject.name+" Crash");
    }

    private void SpeedUp()
    {
        _currentSpeed = Mathf.Clamp(currentSpeed +accelerationRate * Time.deltaTime, 0, currentMaxSpeed);
        dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = _currentSpeed;
    }

    private void Brake()
    {
        _currentSpeed = Mathf.Clamp(currentSpeed-brakeRate * Time.deltaTime, 0, maxSpeed);
        dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = _currentSpeed;
    }


    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2.0f);
        dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = 0;

    }

}

