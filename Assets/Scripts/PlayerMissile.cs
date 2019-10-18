using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour {
	
	private AudioSource DestroyedEnemySound;
	private AudioSource NextLevelReachedSound;
	
	private GameObject[] Enemies;
	private GameObject closestEnemy = null;
	private float distance = Mathf.Infinity;
	private Vector3 difference;
	private float currentDistance;
	
	private float minEnemyX;
	private float minEnemyY;
	private float maxEnemyX;
	private float maxEnemyY;
	
	private GameObject Coin;
	
	// Use this for initialization
	void Start () {
		DestroyedEnemySound = (AudioSource) GameObject.Find("Sounds/DestroyedEnemySound").GetComponent(typeof(AudioSource));
		NextLevelReachedSound = (AudioSource) GameObject.Find("Sounds/NextLevelReachedSound").GetComponent(typeof(AudioSource));
		
		Coin = GameObject.Find("Coin");
		
		StartCoroutine(DestroyTimer());
	}
	
	void FixedUpdate () {
		Enemies = GameObject.FindGameObjectsWithTag("Enemy");
		closestEnemy = null;
		foreach(GameObject Enemy in Enemies)
		{
			difference = Enemy.transform.position - transform.position;
			currentDistance = difference.sqrMagnitude;
			if(currentDistance < distance)
			{
				closestEnemy = Enemy;
				distance = currentDistance;
			}
		}
		
		if(closestEnemy != null)
		{
			minEnemyX = closestEnemy.transform.position.x - closestEnemy.transform.localScale.x;
			minEnemyY = closestEnemy.transform.position.y - closestEnemy.transform.localScale.y;
			maxEnemyX = closestEnemy.transform.position.x + closestEnemy.transform.localScale.x;
			maxEnemyY = closestEnemy.transform.position.y + closestEnemy.transform.localScale.y;
			
			if(transform.position.x >= minEnemyX && transform.position.y >= minEnemyY && transform.position.x <= maxEnemyX && transform.position.y <= maxEnemyY)
			{
				DestroyedEnemySound.Play();
				PlayerPrefs.SetInt("KilledEnemies", PlayerPrefs.GetInt("KilledEnemies") + 1);
				if(PlayerPrefs.GetInt("KilledEnemies") >= PlayerPrefs.GetInt("Level"))
				{
					NextLevelReachedSound.Play();
					PlayerPrefs.SetInt("KilledEnemies", 0);
					PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
				}
				Instantiate(Coin, closestEnemy.transform.position, Quaternion.identity);
				Destroy(closestEnemy);
			}
		}
	}
	
	IEnumerator DestroyTimer ()
	{
		if(PlayerPrefs.GetInt("OriginalMissile") == 1)
		{
			PlayerPrefs.SetInt("OriginalMissile", 0);
		}
		else
		{
			yield return new WaitForSeconds(1);
			Destroy(gameObject);
		}
	}
}
