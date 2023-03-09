using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
	public GameObject[] npcPrefabs;
	
	public GameObject[] spawnPoints;
	
	[SerializeField]
	private float spawnTime = 5;
	[SerializeField]
	private float probSpawn = 0.8f;
	
	[SerializeField]
	private float awayFromSpawn = 3;
	
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(TimeToSpawn());
	}
	
	IEnumerator TimeToSpawn() 
	{
		// Spawn every point
		RandomToSpawnNpc(spawnPoints[0], spawnPoints[1]);
		RandomToSpawnNpc(spawnPoints[1], spawnPoints[0]);
		
		yield return new WaitForSeconds(spawnTime);
		StartCoroutine(TimeToSpawn());
	}
	
	// Spawn npc every spawn times
	void RandomToSpawnNpc(GameObject spawnPoint, GameObject destination) 
	{
		// Random spawn point and destination
		float randomSpawn = Random.Range(0.0f, 1.0f);
		
		if (randomSpawn < probSpawn) {
			// Random an npc to spawn
			int randomIndex = Random.Range(0, npcPrefabs.Length);
			GameObject npc = Instantiate(
				npcPrefabs[randomIndex],
				spawnPoint.transform.position,
				spawnPoint.transform.rotation
			);
			
			npc.transform.Translate(Vector3.forward * awayFromSpawn);
			npc.GetComponent<NPC>().destination = destination;
		}
		
	}
}
