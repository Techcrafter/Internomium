using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
	
	private GameObject PlayerShip;
	private PlayerManagement PlayerManagement;
	
	private GameObject EnemyMissile;
	private bool ableToShoot;
	
	private AudioSource ShootEnemyMissileSound;
	private AudioSource DestroyedEnemySound;
	private AudioSource NextLevelReachedSound;
	
	private bool silent;
	
	public float moveSpeed;
	public float rotateSpeed;
	
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
	
	private float minPlayerMissileX;
	private float minPlayerMissileY;
	private float maxPlayerMissileX;
	private float maxPlayerMissileY;
	
	private GameObject Coin;
	private GameObject HpBooster;
	
	// Use this for initialization
	void Start () {
		PlayerShip = GameObject.Find("PlayerShip");
		PlayerManagement = (PlayerManagement) PlayerShip.GetComponent(typeof(PlayerManagement));
		
		EnemyMissile = GameObject.Find("EnemyMissile");
		
		ShootEnemyMissileSound = (AudioSource) GameObject.Find("Sounds/ShootEnemyMissileSound").GetComponent(typeof(AudioSource));
		DestroyedEnemySound = (AudioSource) GameObject.Find("Sounds/DestroyedEnemySound").GetComponent(typeof(AudioSource));
		NextLevelReachedSound = (AudioSource) GameObject.Find("Sounds/NextLevelReachedSound").GetComponent(typeof(AudioSource));
		
		xChange = Random.Range(-moveSpeed, moveSpeed);
		yChange = Random.Range(-moveSpeed, moveSpeed);
		
		Coin = GameObject.Find("Coin");
		HpBooster = GameObject.Find("HpBooster");
		
		if(PlayerPrefs.GetInt("OriginalEnemy") == 1)
		{
			PlayerPrefs.SetInt("OriginalEnemy", 0);
			silent = true;
		}
		
		StartCoroutine(CooldownTimer());
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerShip.transform.position.x < transform.position.x)
		{
			if(Random.Range(1, 4) == 3)
			{
				transform.Rotate(0, 0, rotateSpeed, Space.Self);
			}
		}
		if(PlayerShip.transform.position.x > transform.position.x)
		{
			if(Random.Range(1, 4) == 3)
			{
				transform.Rotate(0, 0, -rotateSpeed, Space.Self);
			}
		}
		
		if(ableToShoot == true)
		{
			ableToShoot = false;
			if(silent == false)
			{
				ShootEnemyMissileSound.Play();
			}
			Instantiate(EnemyMissile, transform.position, transform.rotation);
			StartCoroutine(CooldownTimer());
		}
		
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
			minPlayerMissileX = closestPlayerMissile.transform.position.x - closestPlayerMissile.transform.localScale.x;
			minPlayerMissileY = closestPlayerMissile.transform.position.y - closestPlayerMissile.transform.localScale.y;
			maxPlayerMissileX = closestPlayerMissile.transform.position.x + closestPlayerMissile.transform.localScale.x;
			maxPlayerMissileY = closestPlayerMissile.transform.position.y + closestPlayerMissile.transform.localScale.y;
			
			if(transform.position.x >= minPlayerMissileX && transform.position.y >= minPlayerMissileY && transform.position.x <= maxPlayerMissileX && transform.position.y <= maxPlayerMissileY)
			{
				DestroyedEnemySound.Play();
				PlayerPrefs.SetInt("KilledEnemies", PlayerPrefs.GetInt("KilledEnemies") + 1);
				if(PlayerPrefs.GetInt("KilledEnemies") >= PlayerPrefs.GetInt("Level"))
				{
					NextLevelReachedSound.Play();
					PlayerPrefs.SetInt("KilledEnemies", 0);
					PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
				}
				if(Random.Range(1, 21) == 1)
				{
					Instantiate(HpBooster, transform.position, Quaternion.identity);
				}
				else
				{
					Instantiate(Coin, transform.position, Quaternion.identity);
				}
				Destroy(gameObject);
			}
		}
	}
	
	IEnumerator CooldownTimer ()
	{
		yield return new WaitForSeconds(Random.Range(1, 4));
		ableToShoot = true;
	}
}
