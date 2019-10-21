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
	
	IEnumerator DestroyTimer ()
	{
		if(PlayerPrefs.GetInt("OriginalPlayerMissile") == 1)
		{
			PlayerPrefs.SetInt("OriginalPlayerMissile", 0);
		}
		else
		{
			yield return new WaitForSeconds(1);
			Destroy(gameObject);
		}
	}
}
