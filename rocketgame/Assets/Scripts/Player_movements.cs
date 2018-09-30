using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movements : MonoBehaviour {

	private Rigidbody2D rb;

	void Awake()
    {
        // SpriteRenderer sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;

        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

	void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
        	rb.velocity = rb.velocity + new Vector2(0.0f, 1.0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
        	rb.velocity = rb.velocity + new Vector2(-1.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
        	rb.velocity = rb.velocity + new Vector2(1.0f, 0.0f);
        }
    }
}
