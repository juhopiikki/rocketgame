using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuStars : MonoBehaviour {

	public GameObject camera;
	private Camera cam;

	private Vector3 inWorld, pos;

	// Use this for initialization
	void Start () {
		cam = camera.GetComponent<Camera>();

		inWorld = cam.ScreenToWorldPoint(new Vector3(cam.nearClipPlane, 0, 0));
		pos = transform.position;

		transform.position = new Vector3(inWorld.x, pos.y, pos.z);
		
	}
}
