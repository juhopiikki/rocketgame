using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astronaut : MonoBehaviour {

	public bool exploded;
	private int frames;
    public GameObject astronautti; 
    public CameraController maincamera;

	// Use this for initialization
	void Start () {
		exploded = false;
		frames = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(exploded) {
			if(frames == 12) {
				astronautti.SetActive(true);
				maincamera.setPlayer(astronautti);
				frames += 1;
			} else {
				frames += 1;
			}
		}		
	}

	public void explode() {
		exploded = true;
        FindObjectOfType<AudioManager>().Stop("engine");
        FindObjectOfType<AudioManager>().Stop("scartch");    
	}
}
