using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNPC : MonoBehaviour
{
    // If NPC has at the destination
	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "NPC") {
			Destroy(other.gameObject);
		}
	}
}
