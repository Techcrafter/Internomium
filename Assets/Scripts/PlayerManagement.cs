using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour {
	
	public GameObject LoadingText;
	
	private GameObject PlayerMissile;
	
	private GameObject Sprite;
	
	private AudioSource GetHitSound;
	private AudioSource ShootPlayerMissileSound;
	private AudioSource CollectCoinSound;
	private AudioSource CollectHpBoosterSound;
	
	private GameObject MusicPlayer;
	
	private int movingSpeed;
	private int rotatingSpeed;
	
	private float cacheX;
	private float cacheY;
	
	private float angle;
	
	private bool HitCooldown;
	
	private GameObject[] Coins;
	private GameObject closestCoin = null;
	private float distance = Mathf.Infinity;
	private Vector3 difference;
	private float currentDistance;
	
	private float minCoinX;
	private float minCoinY;
	private float maxCoinX;
	private float maxCoinY;
	
	private GameObject[] HpBoosters;
	private GameObject closestHpBooster = null;
	
	private float minHpBoosterX;
	private float minHpBoosterY;
	private float maxHpBoosterX;
	private float maxHpBoosterY;
	
	// Use this for initialization
	void Start () {
		PlayerMissile = GameObject.Find("PlayerMissile");
		
		Sprite = GameObject.Find("PlayerShip/Sprite");
		
		GetHitSound = (AudioSource) GameObject.Find("Sounds/GetHitSound").GetComponent(typeof(AudioSource));
		ShootPlayerMissileSound = (AudioSource) GameObject.Find("Sounds/ShootPlayerMissileSound").GetComponent(typeof(AudioSource));
		CollectCoinSound = (AudioSource) GameObject.Find("Sounds/CollectCoinSound").GetComponent(typeof(AudioSource));
		CollectHpBoosterSound = (AudioSource) GameObject.Find("Sounds/CollectHpBoosterSound").GetComponent(typeof(AudioSource));
		
		MusicPlayer = GameObject.Find("MusicPlayer");
		
		movingSpeed = PlayerPrefs.GetInt("MovingSpeed");
		rotatingSpeed = PlayerPrefs.GetInt("RotatingSpeed");
		
		cacheX = transform.position.x;
		cacheY = transform.position.y;
		
		if(PlayerPrefs.GetInt("MusicMuted") == 1)
		{
			MusicPlayer.SetActive(false);
		}
	}
	
	void FixedUpdate () {
		angle = transform.eulerAngles.magnitude * Mathf.Deg2Rad;
		angle += 1.6f;
		
		if(Input.GetAxis("Horizontal") < 0)
		{
			transform.Rotate(0, 0, rotatingSpeed, Space.Self);
		}
		if(Input.GetAxis("Horizontal") > 0)
		{
			transform.Rotate(0, 0, -rotatingSpeed, Space.Self);
		}
		if(Input.GetAxis("Vertical") < 0)
		{
			cacheX -= (Mathf.Cos (angle) * movingSpeed) * Time.deltaTime;
			cacheY -= (Mathf.Sin (angle) * movingSpeed) * Time.deltaTime;
			
			if(cacheX > -9.7f && cacheX < 9.7f)
			{
				transform.position = new Vector3(cacheX, transform.position.y, 0);
			}
			if(cacheY > -4.5f && cacheY < 4.5f)
			{
				transform.position = new Vector3(transform.position.x, cacheY, 0);
			}
		}
		if(Input.GetAxis("Vertical") > 0)
		{
			cacheX += (Mathf.Cos (angle) * movingSpeed) * Time.deltaTime;
			cacheY += (Mathf.Sin (angle) * movingSpeed) * Time.deltaTime;
			
			if(cacheX > -9.7f && cacheX < 9.7f)
			{
				transform.position = new Vector3(cacheX, transform.position.y, 0);
			}
			if(cacheY > -4.5f && cacheY < 4.5f)
			{
				transform.position = new Vector3(transform.position.x, cacheY, 0);
			}
		}
		if(Input.GetButtonDown("Fire1"))
		{
			ShootPlayerMissileSound.Play();
			Instantiate(PlayerMissile, transform.position, transform.rotation);
		}
		if(Input.GetButtonDown("Cancel"))
		{
			LoadingText.SetActive(true);
			Application.LoadLevel(3);
		}
		if(Input.GetButtonDown("Mute music"))
		{
			if(PlayerPrefs.GetInt("MusicMuted") == 0)
			{
				PlayerPrefs.SetInt("MusicMuted", 1);
				MusicPlayer.SetActive(false);
			}
			else
			{
				PlayerPrefs.SetInt("MusicMuted", 0);
				MusicPlayer.SetActive(true);
			}
		}
		
		Coins = GameObject.FindGameObjectsWithTag("Coin");
		closestCoin = null;
		foreach(GameObject Coin in Coins)
		{
			difference = Coin.transform.position - transform.position;
			currentDistance = difference.sqrMagnitude;
			if(currentDistance < distance)
			{
				closestCoin = Coin;
				distance = currentDistance;
			}
		}
		
		if(closestCoin != null)
		{
			minCoinX = closestCoin.transform.position.x - closestCoin.transform.localScale.x;
			minCoinY = closestCoin.transform.position.y - closestCoin.transform.localScale.y;
			maxCoinX = closestCoin.transform.position.x + closestCoin.transform.localScale.x;
			maxCoinY = closestCoin.transform.position.y + closestCoin.transform.localScale.y;
			
			if(transform.position.x >= minCoinX && transform.position.y >= minCoinY && transform.position.x <= maxCoinX && transform.position.y <= maxCoinY)
			{
				CollectCoinSound.Play();
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 20);
				Destroy(closestCoin);
			}
		}
		
		HpBoosters = GameObject.FindGameObjectsWithTag("HpBooster");
		closestHpBooster = null;
		foreach(GameObject HpBooster in HpBoosters)
		{
			difference = HpBooster.transform.position - transform.position;
			currentDistance = difference.sqrMagnitude;
			if(currentDistance < distance)
			{
				closestHpBooster = HpBooster;
				distance = currentDistance;
			}
		}
		
		if(closestHpBooster != null)
		{
			minHpBoosterX = closestHpBooster.transform.position.x - closestHpBooster.transform.localScale.x;
			minHpBoosterY = closestHpBooster.transform.position.y - closestHpBooster.transform.localScale.y;
			maxHpBoosterX = closestHpBooster.transform.position.x + closestHpBooster.transform.localScale.x;
			maxHpBoosterY = closestHpBooster.transform.position.y + closestHpBooster.transform.localScale.y;
			
			if(transform.position.x >= minHpBoosterX && transform.position.y >= minHpBoosterY && transform.position.x <= maxHpBoosterX && transform.position.y <= maxHpBoosterY)
			{
				CollectHpBoosterSound.Play();
				PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP") + 1);
				Destroy(closestHpBooster);
			}
		}
	}
	
	public void TakeHit()
	{
		if(HitCooldown == false)
		{
			HitCooldown = true;
			PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP") - 1);
			if(PlayerPrefs.GetInt("HP") <= 0)
			{
				LoadingText.SetActive(true);
				Application.LoadLevel(3);
			}
			GetHitSound.Play();
			StartCoroutine(HitCooldownTimer());
		}
	}
	
	IEnumerator HitCooldownTimer()
	{
		yield return new WaitForSeconds(0.25f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.25f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.25f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.25f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.125f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		Sprite.SetActive(true);
		HitCooldown = false;
	}
}
