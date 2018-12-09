using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkAnimation : MonoBehaviour {

	public Texture2D[] frames;
	public int framesPerSecond;
	public Plane img;

	// Use this for initialization
	void Start () {
		framesPerSecond = 2;
	}
	
	// Update is called once per frame
	void Update () {
		int index = (int) Time.time * framesPerSecond;
		index = index % frames.Length; 
//		img.renderer.material.mainTexture = frames[index];
		//renderer.material.mainTexture = frames[index];
	}
}
