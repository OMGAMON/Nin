using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonEffect : MonoBehaviour {
	public AudioClip enterSoundClip;
	private AudioSource enterSoundSource;

	void Start() {
		enterSoundSource = GetComponent<AudioSource> ();
	}

	public void playEnterSound() {
		enterSoundSource.PlayOneShot (enterSoundClip, 1f);
	}
}
