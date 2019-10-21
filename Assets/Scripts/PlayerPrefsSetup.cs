using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("MovingSpeed") == 0 || PlayerPrefs.GetInt("RotatingSpeed") == 0)  //Check for first start
		{
			advancedSetup();
		}
	}
	
	public void setupForNewGame()
	{
		PlayerPrefs.SetInt("HP", 3);
		PlayerPrefs.SetInt("KilledEnemies", 0);
		PlayerPrefs.SetInt("OriginalPlayerMissile", 1);
		PlayerPrefs.SetInt("OriginalEnemyMissile", 1);
		PlayerPrefs.SetInt("Level", 1);
	}
	
	public void advancedSetup()
	{
		PlayerPrefs.SetInt("Coins", 0);
		PlayerPrefs.SetInt("Highscore", 0);
		PlayerPrefs.SetInt("MovingSpeed", 1);
		PlayerPrefs.SetInt("RotatingSpeed", 1);
		PlayerPrefs.SetInt("MusicMuted", 0);
	}
}
