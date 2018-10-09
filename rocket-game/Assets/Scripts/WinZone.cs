using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinZone : MonoBehaviour {

	// text object for winning text
	public Text win;

	// Use this for initialization
	void Start () {
    	win.gameObject.SetActive(false);
	}

	// on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
    	Debug.Log("WIN");
    	win.gameObject.SetActive(true);
    }
}
