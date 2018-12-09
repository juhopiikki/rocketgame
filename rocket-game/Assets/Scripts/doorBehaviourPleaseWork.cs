using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorBehaviourPleaseWork : MonoBehaviour {

    public GameObject door, key;
    public keyBehaviour keyBehaviour;
	public GameObject keyUI;
	private float initX, initY; 
	public float moveX, moveY;
	private float newPosX, newPosY;
	Vector3 startPosition, endPosition, tempPosition;

	public float moveSeconds = 2;
	private float moveTimer = 0;
	private bool doorOpened = false;
	private bool trigger = false;
	private float ratio = 0;
	private bool keyCheck = false;

	void Awake()
	{
		initX = door.transform.position.x;
		initY = door.transform.position.y;
		newPosX = initX + moveX;
    	newPosY = initY + moveY;
    	startPosition = door.transform.position;
		endPosition = new Vector3 (newPosX, newPosY, door.transform.position.z);
		//Debug.Log("Door awakened");
	}

	public void KeyFound()
	{
		keyCheck = true;
		FindObjectOfType<AudioManager>().PlayMusic("pickup");
		Debug.Log("KeyFound");
	}

	private void StopMovingAway()
	{
		doorOpened = true;
		door.transform.position = endPosition;
	}

	// on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
    	if(keyBehaviour != null) {
	    	if(keyBehaviour.found) {
	    		keyCheck = true;
	    	}
    	}
    	if(!trigger && keyCheck)
    	{
    		moveTimer = 0;
    		trigger = true;
    		FindObjectOfType<AudioManager>().PlayMusic("dooropening");
    		Debug.Log("OnTriggerEnter2D ovi triggered");
    	}
    }
    	

   	void Update()
   	{
   		if(!doorOpened && trigger)
   		{	
   			moveTimer += Time.deltaTime;

   			if(moveTimer > moveSeconds)
		   	{
		   		StopMovingAway();
		   		Debug.Log("Door opened");
		   	}
		   	else
		   	{
		   		ratio = moveTimer / moveSeconds;
		   		door.transform.position = Vector3.Lerp(startPosition, endPosition, ratio);
		   	}
   		}
   	}
}