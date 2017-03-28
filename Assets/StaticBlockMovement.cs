using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockMovement : MonoBehaviour {
	private Vector3 startingPosition;
	public float blockSpeed;
	public float time;
	public float max;
	public float min;
	private BG_Scrolling backgroundScript;

	void Start () {
		startingPosition = transform.position;
		max = 5;
		min = 3;
		backgroundScript = GameObject.Find ("Backgrounds").GetComponent<BG_Scrolling>();
	}

	void FixedUpdate () {
		time = backgroundScript.time;
		blockSpeed = (max - min) * (1 - Mathf.Exp (-(Time.time - time) / 100f)) + min;
		transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left;
		if (transform.position.x < startingPosition.x - 12) {
			//ensures that after traveling 12 units to left, such sample block goes back to the original position
			//the limit to destroy this block is 13.53
			transform.position = startingPosition;
		}
	}
}
