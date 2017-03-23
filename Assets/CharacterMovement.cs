using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float path;

	private GameObject target;
	private GameObject steadyPoint;
	private TargetSelection targetSelection;

	void Start () {
		target = GameObject.Find ("Target");
		steadyPoint = GameObject.Find ("SteadyPoint");
		targetSelection = target.GetComponent<TargetSelection> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y < 1.6f && transform.position.y > 1.2f) {
			path = 1f;
		} else if (transform.position.y < 0.2f && transform.position.y > -0.2f) {
			path = 2f;
		} else if (transform.position.y < -1.0f && transform.position.y > -1.4f) {
			path = 3f;
		}

		if (targetSelection.newTarget) {
			transform.position = target.transform.position;
			targetSelection.newTarget = false;
		}

		if (transform.position.x > steadyPoint.transform.position.x) {
			transform.position = transform.position + 0.2f * Vector3.left * (transform.position.x - steadyPoint.transform.position.x) * Time.fixedDeltaTime;
		}
	}
}
