using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
	
	public GameObject MovementUpgrade;
	public Text MovementLevel;
	public Text MovementMoney;
	
	public GameObject RotationUpgrade;
	public Text RotationLevel;
	public Text RotationMoney;
	
	private int cache;
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("MovingSpeed") == 10)
		{
			MovementUpgrade.SetActive(false);
		}
		else
		{
			cache = PlayerPrefs.GetInt("MovingSpeed") + 1;
			MovementLevel.text = cache.ToString();
			cache *= 100;
			MovementMoney.text = cache.ToString();
		}
		
		if(PlayerPrefs.GetInt("RotatingSpeed") == 10)
		{
			RotationUpgrade.SetActive(false);
		}
		else
		{
			cache = PlayerPrefs.GetInt("RotatingSpeed") + 1;
			RotationLevel.text = cache.ToString();
			cache *= 100;
			RotationMoney.text = cache.ToString();
		}
	}
	
	public void BuyUpgrade(int id)
	{
		if(id == 1)  //Movement
		{
			cache = PlayerPrefs.GetInt("MovingSpeed") + 1;
			cache *= 100;
			
			if(PlayerPrefs.GetInt("Coins") >= cache)
			{
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - cache);
				PlayerPrefs.SetInt("MovingSpeed", PlayerPrefs.GetInt("MovingSpeed") + 1);
			}
		}
		else if(id == 2)  //Rotation
		{
			cache = PlayerPrefs.GetInt("RotatingSpeed") + 1;
			cache *= 100;
			
			if(PlayerPrefs.GetInt("Coins") >= cache)
			{
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - cache);
				PlayerPrefs.SetInt("RotatingSpeed", PlayerPrefs.GetInt("RotatingSpeed") + 1);
			}
		}
	}
}
