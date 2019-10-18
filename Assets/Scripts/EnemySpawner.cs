using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	
	private int count;
	private bool ableToSpawn = true;
	private int i;
	private bool waitingForNextLevel;
	
	private GameObject Enemy;
	
	private GameObject Spawnpoint1;
	private GameObject Spawnpoint2;
	private GameObject Spawnpoint3;
	private GameObject Spawnpoint4;
	
	// Use this for initialization
	void Start () {
		Enemy = GameObject.Find("Enemy");
		Spawnpoint1 = GameObject.Find("EnemySpawner/Spawnpoint1");
		Spawnpoint2 = GameObject.Find("EnemySpawner/Spawnpoint2");
		Spawnpoint3 = GameObject.Find("EnemySpawner/Spawnpoint3");
		Spawnpoint4 = GameObject.Find("EnemySpawner/Spawnpoint4");
	}
	
	// Update is called once per frame
	void Update () {
		if(count != PlayerPrefs.GetInt("Level") && waitingForNextLevel == false)
		{
			if(ableToSpawn == true)
			{
				ableToSpawn = false;
				i = Random.Range(1, 5);
				if(i == 1)
				{
					Instantiate(Enemy, Spawnpoint1.transform.position, Quaternion.identity);
				}
				else if(i == 2)
				{
					Instantiate(Enemy, Spawnpoint2.transform.position, Quaternion.identity);
				}
				else if(i == 3)
				{
					Instantiate(Enemy, Spawnpoint3.transform.position, Quaternion.identity);
				}
				else if(i == 4)
				{
					Instantiate(Enemy, Spawnpoint4.transform.position, Quaternion.identity);
				}
				StartCoroutine(WaitForNextSpawn());
				count++;
			}
		}
		else if(count != PlayerPrefs.GetInt("Level") && waitingForNextLevel == true)
		{
			waitingForNextLevel = false;
			count = 0;
		}
		else
		{
			waitingForNextLevel = true;
		}
	}
	
	IEnumerator WaitForNextSpawn ()
	{
		yield return new WaitForSeconds(Random.Range(5, 16));
		ableToSpawn = true;
	}
}
