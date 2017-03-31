using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmControl : MonoBehaviour {

	private StaticBlockMovement blockScript;
	public float initialSpeed;
	public float currentSpeed;
	private AudioSource bgmSource;
	// Use this for initialization
	void Start () {
		bgmSource = GetComponent<AudioSource> ();
		blockScript = GameObject.Find ("static block").GetComponent<StaticBlockMovement>();	
		initialSpeed = blockScript.blockSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		currentSpeed = blockScript.blockSpeed;
		bgmSource.pitch = (currentSpeed / initialSpeed - 1) * 0.2f + 1;
		
	}
}
