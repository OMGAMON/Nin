using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockMovement : MonoBehaviour {
	private Vector3 startingPosition;
	public float blockSpeed = 0.7f;

	void Start () {
		startingPosition = transform.position;
	}

	void FixedUpdate () {
		transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left;
		if (transform.position.x < startingPosition.x - 12) {
			//ensures that after traveling 12 units to left, such sample block goes back to the original position
			//the limit to destroy this block is 13.53
			transform.position = startingPosition;
		}
	}
}
