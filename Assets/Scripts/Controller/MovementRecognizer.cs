using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;
public enum MovementStates
{
    Stop,
    Call,
    None
}

public class MovementRecognizer : MonoBehaviour
{
    private bool isMoving = false;
    private bool isPressed = false;
    public Transform movementSource;
    private List<Vector3> positionsList = new List<Vector3>();
    private List<Gesture> trainingSet = new List<Gesture>();
    public float newPositionThresholdDistance = 0.05f;
    public GameObject debugCubePrefab;
    public bool creationMode = true;
    public string newGestureName;
    public float recognitionThreshold = 0.9f;
    private MovementStates command = MovementStates.None;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    public UnityStringEvent OnRecognized;


    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.streamingAssetsPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    public void onCallPressed()
    {
        Debug.Log("now call pressed");
        isPressed = true;
        command = MovementStates.Call;

    }
    public void onReleased()
    {
        isPressed = false;
        //command = MovementStates.None;
    }
    public void onStopPressed()
    {
        Debug.Log("now stop pressed");
        isPressed = true;
        command = MovementStates.Stop;
    }

    // Update is called once per frame
    void Update()
    {
        
        //InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        //Start The Movement
        if(!isMoving && isPressed)
        {
            StartMovement();
        }
        else if(isMoving && !isPressed)
        {
            EndMovement();
        }
        //Updating The Movement
        else if(isMoving && isPressed)
        {
            UpdateMovement();
        }
    }
    void StartMovement()
    {
        //Debug.Log("Start Movement");
        isMoving = true;
        positionsList.Clear();
        positionsList.Add(movementSource.position);
        if (debugCubePrefab)
        {
            Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity),3);
        }
       
    }
    void EndMovement()
    {
        //Debug.Log("End Movement");
        isMoving = false;

        //Create The Gesture From The Position List
        Point[] pointArray = new Point[positionsList.Count];

        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);

        //Add A new gesture to training set
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        }
        //recognize
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(command == MovementStates.Stop && result.GestureClass == "stop");
            if (command == MovementStates.Call && result.GestureClass == "call")
            {
                if (result.Score > recognitionThreshold)
                {
                    OnRecognized.Invoke(result.GestureClass);
                    Debug.Log(result.GestureClass + result.Score);
                }
            }
            if (command == MovementStates.Stop && result.GestureClass == "stop")
            {
                if (result.Score > recognitionThreshold)
                {
                    OnRecognized.Invoke(result.GestureClass);
                    Debug.Log(result.GestureClass + result.Score);
                }
            }
                
            
        }
    }
    void UpdateMovement()
    {
        //Debug.Log("Update Movement");
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        if(Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
        {
            positionsList.Add(movementSource.position);
            if (debugCubePrefab)
            {
                Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
            }
        }
        
    }
}
