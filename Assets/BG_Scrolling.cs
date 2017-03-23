using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour {
	public float scrollingSpeed = 0.5f;
	public float tileLength = 7.834f;	//total length of one tile (the background is made up of 2 tiles)

	private Vector3 startingPosition;

	void Start () {
		startingPosition = transform.position;
	}
	
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * scrollingSpeed, tileLength);
		transform.position = startingPosition + newPosition * Vector3.left;
	}
}
