using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTutorialBackground : MonoBehaviour {

	public GameObject player;

	private float offset;
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(player.transform.position[0], transform.position[1], transform.position[2]);
	}
}
