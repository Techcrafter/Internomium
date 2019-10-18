using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalLevelAndHighscoreDisplay : MonoBehaviour {
	
	public Text FinalLevelCounter;
	public GameObject NewHighscoreText;
	
	// Use this for initialization
	void Start () {
		FinalLevelCounter.text = PlayerPrefs.GetInt("Level").ToString();
		
		if(PlayerPrefs.GetInt("Level") > PlayerPrefs.GetInt("Highscore"))
		{
			PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("Level"));
			NewHighscoreText.SetActive(true);
		}
	}
}
