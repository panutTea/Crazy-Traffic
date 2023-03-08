using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
public class RayHit : MonoBehaviour
{
    public string interactableTag = "Car";
    public bool isHovered = false;
    private HoverEnterEventArgs nowArgs;

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"{args.interactorObject} hovered over {args.interactableObject}", this);
        isHovered = true;
        nowArgs = args;
    }
    public void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"{args.interactorObject} hovered over {args.interactableObject}", this);
        //isHovered = false;
    }

    public void ReceiveCommand(string command)
    {
        Debug.Log(command + " " + isHovered);
        if(command == "stop" && isHovered)
        {
            nowArgs.interactableObject.transform.GetComponent<Car>().StopCar();
        }
        if(command == "call" && isHovered)
        {
            nowArgs.interactableObject.transform.GetComponent<Car>().ReleaseCar();
        }
    }
}
