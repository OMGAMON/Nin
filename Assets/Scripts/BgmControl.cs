using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmControl : MonoBehaviour {


	private StaticBlockMovement blockScript;
	private float initialTime;
	private float currentTime;
	private AudioSource bgmSource;
	// Use this for initialization
	void Start () {
		bgmSource = GetComponent<AudioSource> ();
		bgmSource.loop = true;
		bgmSource.Play ();
		initialTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.name == "GameBGM") {
			currentTime = Time.time;
			bgmSource.pitch = (currentTime - initialTime) / 600f + 1;
		}
	}
}
