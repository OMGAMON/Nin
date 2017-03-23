using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float path;
	// Use this for initialization

	void Start () {
		
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
	}
}
