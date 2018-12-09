using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollBG : MonoBehaviour {

	public float speed = 0.05f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2 (Time.time * speed, 0);
		GetComponent<Renderer>().material.mainTextureOffset = offset;

	}


	//public Vector2 offset = new Vector2(0,0);
    //public float scrollSpeed = 1.0f;
    //public void Update() {
        //offset += new Vector2(Time.deltaTime * Input.GetAxis("Horizontal")  *  -1 * scrollSpeed , 0);
          //assign offset here
    //}
}
