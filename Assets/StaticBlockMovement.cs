using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockMovement : MonoBehaviour {
	private Vector3 startingPosition;
	public float blockSpeed;

	void Start () {
		startingPosition = transform.position;
	}

	void FixedUpdate () {
		blockSpeed = 5.3f * (1 - Mathf.Exp (-Time.fixedTime / 100f)) + 0.7f;
		transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left;
		if (transform.position.x < startingPosition.x - 12) {
			//ensures that after traveling 12 units to left, such sample block goes back to the original position
			//the limit to destroy this block is 13.53
			transform.position = startingPosition;
		}
	}
}
