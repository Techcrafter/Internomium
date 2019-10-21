using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSprite : MonoBehaviour {
	
	private GameObject PlayerShip;
	private PlayerManagement PlayerManagement;
	
	private float minPlayerX;
	private float minPlayerY;
	private float maxPlayerX;
	private float maxPlayerY;
	
	// Use this for initialization
	void Start () {
		PlayerShip = GameObject.Find("PlayerShip");
		PlayerManagement = (PlayerManagement) PlayerShip.GetComponent(typeof(PlayerManagement));
	}
	
	void FixedUpdate()
	{
		minPlayerX = PlayerShip.transform.position.x - PlayerShip.transform.localScale.x;
		minPlayerY = PlayerShip.transform.position.y - PlayerShip.transform.localScale.y;
		maxPlayerX = PlayerShip.transform.position.x + PlayerShip.transform.localScale.x;
		maxPlayerY = PlayerShip.transform.position.y + PlayerShip.transform.localScale.y;
		
		if(transform.position.x >= minPlayerX && transform.position.y >= minPlayerY && transform.position.x <= maxPlayerX && transform.position.y <= maxPlayerY)
		{
			PlayerManagement.TakeHit();
		}
	}
}
