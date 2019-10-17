using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour {
	
	private Text CounterText;
	
	// Use this for initialization
	void Start () {
		CounterText = gameObject.GetComponent(typeof(Text)) as Text;
	}
	
	// Update is called once per frame
	void Update () {
		CounterText.text = PlayerPrefs.GetInt("Coins").ToString();
	}
}
