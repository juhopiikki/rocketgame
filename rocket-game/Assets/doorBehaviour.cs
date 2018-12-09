using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newDoorBehaviour : MonoBehaviour {

    public GameObject door;
	public float initX, initY, moveX, moveY;

	// on collision
    void OnTriggerEnter2D(Collider2D collider)
    {
    	door.transform.position = new Vector3 (moveX, moveY, transform.position.z);
    	
    	//while((door.velocity.position.x - moveX))
    }
}
