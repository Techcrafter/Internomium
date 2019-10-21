using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyTimer());
	}
	
	IEnumerator DestroyTimer ()
	{
		if(PlayerPrefs.GetInt("OriginalEnemyMissile") == 1)
		{
			PlayerPrefs.SetInt("OriginalEnemyMissile", 0);
		}
		else
		{
			yield return new WaitForSeconds(1);
			Destroy(gameObject);
		}
	}
}
