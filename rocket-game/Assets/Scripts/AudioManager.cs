using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	// Use this for initialization
	void Awake () {
		foreach(Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
	
	public void Play (string name) {
		if(PlayerPrefs.GetInt("soundOn") == 1) {
			Sound s = Array.Find(sounds, sound => sound.name == name);
			if (s == null)
				return;
			if(!s.source.isPlaying) {
				s.source.Play();
			}
		}
	}

	public void PlayMusic (string name) {
		if(PlayerPrefs.GetInt("musicOn") == 1) {
			Sound s = Array.Find(sounds, sound => sound.name == name);
			if (s == null)
				return;
			if(!s.source.isPlaying) {
				s.source.Play();
			}
		}
	}

	public void PauseMusic (string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
			return;
		if(s.source.isPlaying) {
			s.source.Pause();
		}
	}	

	public void UnPauseMusic (string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
			return;
		if(!s.source.isPlaying) {
			s.source.UnPause();
		}
		if(!s.source.isPlaying) {
			s.source.Play();
		}
	}	

	public void Stop (string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.Log("Sound not found");
			return;			
		}
		s.source.Stop();
	}
}
