using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	private int shakeNow;
	private float shakePower, shakeMultiplier;

	public bool meteoroidLevel;
	public bool escapeLvl;
	private float initY;
	private bool astro;
	private float astroScale;

	//public Camera this_camera;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		shakeNow = 0;
		shakePower = 1.0f;
		shakeMultiplier = 0.02f;
		initY = transform.position.y;
		astro = false;
		astroScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(escapeLvl) {
			if(offset.y > -10) {
				offset = offset - new Vector3(0f, player.GetComponent<Rigidbody2D>().velocity.y * 0.0005f, 0f);
				Camera.main.orthographicSize = Camera.main.orthographicSize + player.GetComponent<Rigidbody2D>().velocity.y * 0.0002f;
			}
		}

		if(shakeNow > 0) {
			transform.position += new Vector3(Random.Range(-shakePower * shakeMultiplier, shakePower * shakeMultiplier), 
				Random.Range(-shakePower * shakeMultiplier, shakePower * shakeMultiplier), 0);
		    shakeNow -= 1;
		} else {
			if(meteoroidLevel) {
				transform.position = new Vector3(player.transform.position.x, Mathf.Max(player.transform.position.y, initY), player.transform.position.z) + offset;
			} else {
				transform.position = player.transform.position + offset;			
			}
		}

		// zoom to astronaut after explosion
		if(astro && astroScale > 0.4f) {
			astroScale = astroScale * 0.99f;
			Camera.main.orthographicSize = Camera.main.orthographicSize * 0.99f;
		}

	}

	public void shake(float power) {
        shakeNow = 3;
        shakePower = power;
	}

	public void setPlayer(GameObject newplayer) {
		player = newplayer;
		offset = transform.position - player.transform.position;
		astro = true;
	}
}
