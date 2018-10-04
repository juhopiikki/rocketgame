using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movements : MonoBehaviour {

	private Rigidbody2D rb;

	public float speedScale;
	public float rotationSpeedScale;
	public float rotationScale;

	void Awake()
    {
        // SpriteRenderer sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;

        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        //rb.bodyType = RigidbodyType2D.Kinematic;
    }

	void Update()
    {
//    	Debug.Log("angular: " + rb.angularVelocity);
//    	Debug.Log("rotation: " + rb.rotation);
//    	Debug.Log("forward: " + rb.transform.forward);
    	Debug.Log("right: " + rb.transform.right);
        if (Input.GetKey(KeyCode.UpArrow))
        {
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * speedScale, rb.transform.right[0] * speedScale);
        	//rb.AddForce(rb.transform.right * 1);
//        	rb.velocity = rb.velocity + new Vector2(rb.trasform.rotation, 0.2f);
//        	rb.angularVelocity = rb.angularVelocity + 0.2f; 
//        	rb.velocity = rb.velocity + new Vector2(0.0f, 0.2f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
        	rb.angularVelocity = rb.angularVelocity + 1.0f * rotationScale;
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
        	//rb.velocity = rb.velocity + new Vector2(-0.2f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
        	rb.angularVelocity = rb.angularVelocity - 1.0f * rotationScale;
        	rb.velocity = rb.velocity + new Vector2(-rb.transform.right[1] * rotationSpeedScale, rb.transform.right[0] * rotationSpeedScale);
        	// rb.velocity = rb.velocity + new Vector2(0.2f, 0.0f);
        }
    }
}
