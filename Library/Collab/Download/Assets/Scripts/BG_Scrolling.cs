using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour {
	public float scrollingSpeed;
	public float tileLength = 7.834f;	//total length of one tile (the background is made up of 2 tiles)
	public float time;

	private Vector3 startingPosition;

	void Start () {
		time = Time.time;	//reference time after creation
		startingPosition = transform.position;
		scrollingSpeed = 0.5f;
	}
	
	void FixedUpdate () {
		float newPosition = Mathf.Repeat (Time.time * scrollingSpeed, tileLength);	//repeat to 0 when offset reaches tileLength
		transform.position = startingPosition + newPosition * Vector3.left;
	}
}
