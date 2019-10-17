using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour {
	
	
	public GameObject Missile;
	
	private int movingSpeed;
	private int rotatingSpeed;
	
	private float cacheX;
	private float cacheY;
	
	private float angle;
	
	// Use this for initialization
	void Start () {
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
			
			transform.position = new Vector3(cacheX, cacheY, 0);
		}
		if(Input.GetAxis("Vertical") > 0)
		{
			cacheX += (Mathf.Cos (angle) * movingSpeed) * Time.deltaTime;
			cacheY += (Mathf.Sin (angle) * movingSpeed) * Time.deltaTime;
			
			transform.position = new Vector3(cacheX, cacheY, 0);
		}
		if(Input.GetButtonDown("Fire1"))
		{
			Instantiate(Missile, transform.position, transform.rotation);
		}
	}
}
