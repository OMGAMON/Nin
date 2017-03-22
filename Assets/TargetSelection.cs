using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {
	public GameObject character;
	//public float speed = 1.5f;
	private Vector3 startingPosition;
	private Vector3 endingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = 1.1f * Vector3.right + character.transform.position;
		endingPosition = 4.57f * Vector3.right + character.transform.position;
		transform.position = startingPosition;
	}

	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			startingPosition = new Vector3 (character.transform.position.x + 1.1f, 1.355f, 5f);
			endingPosition = new Vector3 (character.transform.position.x + 4.57f, 1.355f, 5f);
			transform.position = startingPosition;
		} else if (Input.GetKeyDown (KeyCode.A)) {
			startingPosition = new Vector3 (character.transform.position.x + 1.1f, 0f, 5f);
			endingPosition = new Vector3 (character.transform.position.x + 4.57f, 0f, 5f);
			transform.position = startingPosition;
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			startingPosition = new Vector3 (character.transform.position.x + 1.1f, -1.256f, 5f);
			endingPosition = new Vector3 (character.transform.position.x + 4.57f, -1.256f, 5f);
			transform.position = startingPosition;
		}

		if (Input.GetKey (KeyCode.Q)) {
			/*if (transform.position.x == startingPosition.x - 0.1f || transform.position.x == endingPosition.x) {
				speed = -1f * speed;
			}
			transform.position = speed * Vector3.right * Time.fixedDeltaTime + transform.position;*/
			transform.position = 1.735f * Mathf.Sin (2f * Mathf.PI * Time.time - Mathf.PI / 2f) * Vector3.right + startingPosition;
		}
			
	}
}
