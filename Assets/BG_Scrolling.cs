using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour {
	public float scrollingSpeed = 0.5f;
	public float tileLength = 7.834f;

	private Vector3 startingPosition;
	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * scrollingSpeed, tileLength);
		transform.position = startingPosition + newPosition * Vector3.left;
	}
}
