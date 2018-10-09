using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

	public float HP;

	// Use this for initialization
	void Start () {
		HP = 100.0f;
		Debug.Log("Collision script start");
	}
	
    // on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        // take HP
        HP -= collision.relativeVelocity.magnitude;
        Debug.Log("HP: " + HP);

        // if the colliding objects had a big impact:
//        if (collision.relativeVelocity.magnitude > 2) {

//        }
            
    }

}
