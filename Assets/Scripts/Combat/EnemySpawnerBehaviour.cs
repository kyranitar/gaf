using UnityEngine;
using System.Collections;

public class EnemySpawnerBehaviour : MonoBehaviour {
	
	/* The Time.time at which a unit was spawned */
	private float vLastSpawnTime = -1;
	
	/* Enemy Prefab to spawn */
	public Transform SpawnEnemy;
	/* Interval (seconds) to spawn enemies */
	public float SpawnInterval = 5;
	
	void Start () {
		/* [SpawnInterval] from now, spawn an enemy */
		vLastSpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		/* If [SpawnInterval] has elapsed, spawn an enemy */
		if((Time.time - vLastSpawnTime) > SpawnInterval){
			Instantiate(SpawnEnemy, transform.position, transform.rotation);
			vLastSpawnTime = Time.time;
		}
	}
}
