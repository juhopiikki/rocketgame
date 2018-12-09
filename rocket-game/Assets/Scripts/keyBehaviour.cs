using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyBehaviour : MonoBehaviour {

	public doorBehaviourPleaseWork doorScript;
	public GameObject keyUI;
	public bool found = false;

	void OnTriggerEnter2D(Collider2D collider)
    {
    	if(!found) 
    	{
    		doorScript.KeyFound();
    		gameObject.SetActive(false);
    		found = true;
			keyUI.SetActive(true);
    	}
    }
}
