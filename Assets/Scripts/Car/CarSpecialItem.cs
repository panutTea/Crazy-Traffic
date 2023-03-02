using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpecialItem : MonoBehaviour
{
	public GameObject[] specialPrefabs;
	private GameObject specialItem;
	
	public GameObject spawnPointObject;

	// Proportunity to spawn the special item range 0.0 - 1.0
	public float spawnProp = 0.5f;

	private Outline outline;
	
	// Start is called before the first frame update
	void Start()
	{
		outline = GetComponent<Outline>();
		RandomSpawn();
	}

	void RandomSpawn() {
		// Random to attach special item to the car
		float random = Random.Range(0, 1.0f);
		outline.enabled = random < spawnProp;

		// Random which special item that will attach
		if (outline.enabled) {
			int randomIndex = Random.Range(0, specialPrefabs.Length);
			specialItem = specialPrefabs[randomIndex];
			outline.OutlineColor = specialItem.GetComponent<SpecialItem>().outlineColor;
		}
	}
	
	public void DropSpecialItem() 
	{
		if (specialItem != null) 
		{
			Vector3 spawnPos = spawnPointObject.transform.position;
			GameObject si = Instantiate(specialItem, spawnPos, specialItem.transform.rotation);
		}
	}
}
