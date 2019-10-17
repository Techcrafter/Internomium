using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {
	
	public GameObject LoadingText;
	
	public void loadScene(int level)
	{
		LoadingText.SetActive(true);
		Application.LoadLevel(level);
	}
	
	public void quitGame()
	{
		LoadingText.SetActive(true);
		Application.Quit();
	}
}
