using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinZone : MonoBehaviour {

	// text and buttons objects for winning menu objects
	public Text winText;
	public GameObject winButtons, winbuttons2;
	public string levelPlrPrefName;

	// Use this for initialization
	void Start () {
		winText.gameObject.SetActive(false);
    	winButtons.SetActive(false);
	}

	// on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerPrefs.SetInt(levelPlrPrefName, 1);
    	winText.gameObject.SetActive(true);
    	winButtons.SetActive(true);
    	if(levelPlrPrefName == "lvl3") {
    		winbuttons2.SetActive(true);
    	}
    }
}
