using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
public class RayHit : MonoBehaviour
{
	public string interactableTag = "Car";
	public bool isHovered = false;
	private HoverEnterEventArgs nowArgs;
	private AudioSource playerAudio;
	public AudioClip whistleAudio;
	public AudioClip whistle2Audio;


	void Start()
	{
		playerAudio = GetComponent<AudioSource>();
	}
	public void OnHoverEntered(HoverEnterEventArgs args)
	{
		//if(args.interactable)
		Debug.Log($"{args.interactorObject} hovered over {args.interactableObject}", this);
		isHovered = true;
		nowArgs = args;
	}
	public void OnHoverExited(HoverExitEventArgs args)
	{
		Debug.Log($"{args.interactorObject} hovered over {args.interactableObject}", this);
		isHovered = false;
	}

	public void ChangeCarState()
    {
		if(nowArgs.interactableObject.transform.GetComponent<Car>().moveState == MoveStates.Moving)
        {
			nowArgs.interactableObject.transform.GetComponent<Car>().StopCar();
			playerAudio.PlayOneShot(whistleAudio, 1.0f);
		}
        else if(nowArgs.interactableObject.transform.GetComponent<Car>().moveState == MoveStates.Stop)
        {
			nowArgs.interactableObject.transform.GetComponent<Car>().ReleaseCar();
			playerAudio.PlayOneShot(whistle2Audio, 1.0f);
		}
	}

	public void ReceiveCommand(string command)
	{
		//Debug.Log(command + " " + isHovered);
		if(command == "stop" && isHovered)
		{
			nowArgs.interactableObject.transform.GetComponent<Car>().StopCar();
		}
		if(command == "call" && isHovered)
		{
			nowArgs.interactableObject.transform.GetComponent<Car>().ReleaseCar();
		}
		if(command == "useHallsCool" && isHovered)
		{
			nowArgs.interactableObject.transform.GetComponent<Car>().ClearAllCarEmotionInLane();
		}
	}
}
