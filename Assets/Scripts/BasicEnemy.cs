using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
	
	private GameObject PlayerShip;
	private PlayerManagement PlayerManagement;
	
	private AudioSource DestroyedEnemySound;
	private AudioSource NextLevelReachedSound;
	
	public float moveSpeed;
	
	private float minPlayerX;
	private float minPlayerY;
	private float maxPlayerX;
	private float maxPlayerY;
	
	private int i;
	public int minMultipleTimes;
	private float cacheX;
	private float cacheY;
	private float xChange;
	private float yChange;
	
	private GameObject[] PlayerMissiles;
	private GameObject closestPlayerMissile = null;
	private float distance = Mathf.Infinity;
	private Vector3 difference;
	private float currentDistance;
	
	private float minMissileX;
	private float minMissileY;
	private float maxMissileX;
	private float maxMissileY;
	
	private GameObject Coin;
	
	// Use this for initialization
	void Start () {
		PlayerShip = GameObject.Find("PlayerShip");
		PlayerManagement = (PlayerManagement) PlayerShip.GetComponent(typeof(PlayerManagement));
		
		DestroyedEnemySound = (AudioSource) GameObject.Find("Sounds/DestroyedEnemySound").GetComponent(typeof(AudioSource));
		NextLevelReachedSound = (AudioSource) GameObject.Find("Sounds/NextLevelReachedSound").GetComponent(typeof(AudioSource));
		
		xChange = Random.Range(-moveSpeed, moveSpeed);
		yChange = Random.Range(-moveSpeed, moveSpeed);
		
		Coin = GameObject.Find("Coin");
	}
	
	// Update is called once per frame
	void Update () {
		minPlayerX = PlayerShip.transform.position.x - PlayerShip.transform.localScale.x;
		minPlayerY = PlayerShip.transform.position.y - PlayerShip.transform.localScale.y;
		maxPlayerX = PlayerShip.transform.position.x + PlayerShip.transform.localScale.x;
		maxPlayerY = PlayerShip.transform.position.y + PlayerShip.transform.localScale.y;
		
		if(transform.position.x >= minPlayerX && transform.position.y >= minPlayerY && transform.position.x <= maxPlayerX && transform.position.y <= maxPlayerY)
		{
			PlayerManagement.TakeHit();
		}
		
		if(i != minMultipleTimes)
		{
			i++;
			cacheX = transform.position.x + xChange;
			cacheY = transform.position.y + yChange;
			if(cacheX > -9.7f && cacheX < 9.7f)
			{
				transform.position = new Vector3(cacheX, transform.position.y, 0);
			}
			else
			{
				xChange = Random.Range(-moveSpeed, moveSpeed);
			}
			if(cacheY > -4.5f && cacheY < 4.5f)
			{
				transform.position = new Vector3(transform.position.x, cacheY, 0);
			}
			else
			{
				yChange = Random.Range(-moveSpeed, moveSpeed);
			}
		}
		else
		{
			xChange = Random.Range(-moveSpeed, moveSpeed);
			yChange = Random.Range(-moveSpeed, moveSpeed);
			i = 0;
		}
	}
	
	void FixedUpdate ()
	{
		PlayerMissiles = GameObject.FindGameObjectsWithTag("PlayerMissile");
		closestPlayerMissile = null;
		foreach(GameObject PlayerMissile in PlayerMissiles)
		{
			difference = PlayerMissile.transform.position - transform.position;
			currentDistance = difference.sqrMagnitude;
			if(currentDistance < distance)
			{
				closestPlayerMissile = PlayerMissile;
				distance = currentDistance;
			}
		}
		
		if(closestPlayerMissile != null)
		{
			minMissileX = closestPlayerMissile.transform.position.x - closestPlayerMissile.transform.localScale.x;
			minMissileY = closestPlayerMissile.transform.position.y - closestPlayerMissile.transform.localScale.y;
			maxMissileX = closestPlayerMissile.transform.position.x + closestPlayerMissile.transform.localScale.x;
			maxMissileY = closestPlayerMissile.transform.position.y + closestPlayerMissile.transform.localScale.y;
			
			if(transform.position.x >= minMissileX && transform.position.y >= minMissileY && transform.position.x <= maxMissileX && transform.position.y <= maxMissileY)
			{
				DestroyedEnemySound.Play();
				PlayerPrefs.SetInt("KilledEnemies", PlayerPrefs.GetInt("KilledEnemies") + 1);
				if(PlayerPrefs.GetInt("KilledEnemies") >= PlayerPrefs.GetInt("Level"))
				{
					NextLevelReachedSound.Play();
					PlayerPrefs.SetInt("KilledEnemies", 0);
					PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
				}
				Instantiate(Coin, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
	}
}
