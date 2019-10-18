using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour {
	
	public GameObject LoadingText;
	
	private GameObject Missile;
	
	private GameObject Sprite;
	
	private AudioSource GetHitSound;
	private AudioSource ShootMissileSound;
	private AudioSource CollectCoinSound;
	
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
	
	// Use this for initialization
	void Start () {
		Missile = GameObject.Find("PlayerMissile");
		
		Sprite = GameObject.Find("PlayerShip/Sprite");
		
		CollectCoinSound = (AudioSource) GameObject.Find("Sounds/CollectCoinSound").GetComponent(typeof(AudioSource));
		GetHitSound = (AudioSource) GameObject.Find("Sounds/GetHitSound").GetComponent(typeof(AudioSource));
		ShootMissileSound = (AudioSource) GameObject.Find("Sounds/ShootMissileSound").GetComponent(typeof(AudioSource));
		
		movingSpeed = PlayerPrefs.GetInt("MovingSpeed");
		rotatingSpeed = PlayerPrefs.GetInt("RotatingSpeed");
		
		cacheX = transform.position.x;
		cacheY = transform.position.y;
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
			ShootMissileSound.Play();
			Instantiate(Missile, transform.position, transform.rotation);
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
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 10);
				Destroy(closestCoin);
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
