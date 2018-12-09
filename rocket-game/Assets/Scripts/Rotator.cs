using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float delta = 0.5f;  // Amount to move up and down from the start point
    public float speed = 1.0f; 
    private Vector3 startPos;
 
    void Start () {
         startPos = transform.position;
    }
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (0,0,45) * Time.deltaTime);
		Vector3 v = startPos;
        v.y += delta * Mathf.Sin (Time.time * speed);
        transform.position = v;
	}
}
