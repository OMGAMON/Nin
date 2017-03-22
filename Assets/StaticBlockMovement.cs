using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockMovement : MonoBehaviour {
	private Vector3 startingPosition;
	public float blockSpeed = 0.7f;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left;
		if (transform.position.x < startingPosition.x - 12) {
			transform.position = startingPosition;
		}
	}
}
