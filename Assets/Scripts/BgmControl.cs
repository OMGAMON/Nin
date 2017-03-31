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
		initialTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		currentTime = Time.time;
		bgmSource.pitch = (currentTime - initialTime) / 600f + 1;

	}
}
