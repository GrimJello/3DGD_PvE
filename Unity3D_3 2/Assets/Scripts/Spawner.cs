using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	/*NOTES:
	 * -look into "pooling" - creating enemies at the beginning of the game and then setting them active when I need them
	 * 
	 */


	public Transform[] spawnLocations;
	public GameObject prefabToSpawn;

	public int[] waveStartTime;
	private bool wave1, wave2, wave3, wave4, wave5, wave6;
	private bool wave1Done,wave2Done,wave3Done,wave4Done,wave5Done,wave6Done;



	float timer = 0;


	void Update(){
		timer += Time.deltaTime;
		//Debug.Log (timer);

		if (timer > waveStartTime[0]) wave1 = true;
		if (timer > waveStartTime[1]) wave2 = true;
		if (timer > waveStartTime[2]) wave3 = true;
		if (timer > waveStartTime[3]) wave4 = true;
		if (timer > waveStartTime[4]) wave5 = true;
		if (timer > waveStartTime[5]) wave6 = true;

		if (wave1 && wave1Done == false) {
			spawnPrefab (0,0);
			spawnPrefab (4,1);
//			spawnPrefab (3,2);
//			spawnPrefab (3,3);
//			spawnPrefab (2,4);
//			spawnPrefab (0,5);
			wave1Done = true;
		}
		if (wave2 && wave2Done == false) {
			spawnPrefab (8,0);
			spawnPrefab (0,1);
//			spawnPrefab (0,2);
//			spawnPrefab (10,3);
//			spawnPrefab (0,4);
//			spawnPrefab (5,5);
			wave2Done = true;
		}
		if (wave3 && wave3Done == false) {
			spawnPrefab (0,0);
			spawnPrefab (16,1);
//			spawnPrefab (10,2);
//			spawnPrefab (0,3);
//			spawnPrefab (10,4);
//			spawnPrefab (0,5);
			wave3Done = true;
		}
		if (wave4 && wave4Done == false) {
			spawnPrefab (10,0);
			spawnPrefab (10,1);
//			spawnPrefab (0,2);
//			spawnPrefab (20,3);
//			spawnPrefab (0,4);
//			spawnPrefab (0,5);
			wave4Done = true;
		}
		if (wave5 && wave5Done == false) {
			spawnPrefab (5,0);
			spawnPrefab (15,1);
//			spawnPrefab (15,2);
//			spawnPrefab (5,3);
//			spawnPrefab (5,4);
//			spawnPrefab (5,5);
			wave5Done = true;
		}
		if (wave6 && wave6Done == false) {
			spawnPrefab (15,0);
			spawnPrefab (15,1);
//			spawnPrefab (15,2);
//			spawnPrefab (15,3);
//			spawnPrefab (15,4);
//			spawnPrefab (15,5);
			wave6Done = true;
		}




	}

	public void spawnPrefab(int amount, int location){
		for (int i = 0; i < amount; i++) {
			
			Instantiate (prefabToSpawn, spawnLocations [location].position, transform.rotation);
		}
			
	}

}
/*
using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour {


	public GameObject prefab;
	public bool active = true;

	public float delay = 2.0f;
	public float rate = 1.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine (Spawn ());
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator Spawn(){
		yield return new WaitForSeconds (delay);

		while (active) {
			Instantiate (prefab,transform.position,transform.rotation);
			yield return new WaitForSeconds (rate);
		}
	}

}
*/
