using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {
	public GameObject character;
	public float period = 5f;
	public bool newTarget;

	private Vector3 startingPosition;
	private Renderer rend;
	private float chrPath;
	private CharacterMovement characterMovement;
	private KeyCode lastKey;
	private bool targetEnabled;
	private bool inSameFrame;


	// Use this for initialization
	void Start () {
		startingPosition = 2.835f * Vector3.right + character.transform.position;
		transform.position = startingPosition;
		character = GameObject.Find ("Character");
		characterMovement = character.GetComponent<CharacterMovement> ();
		rend = GetComponent<Renderer>();
		rend.enabled = false;
		newTarget = false;
	}

	void Update () {
		inSameFrame = false;
		chrPath = characterMovement.path;
		if (Input.GetKeyDown(KeyCode.Q) && (!targetEnabled || !Input.GetKeyDown(lastKey)) && chrPath != 1) {
			startingPosition = new Vector3 (character.transform.position.x + 2.835f, 1.355f, 5f);
			SetPosition ();
			lastKey = KeyCode.Q;
		} else if (Input.GetKeyDown(KeyCode.A) && (!targetEnabled || !Input.GetKeyDown(lastKey)) && chrPath != 2) {
			startingPosition = new Vector3 (character.transform.position.x + 2.835f, 0f, 5f);
			SetPosition ();
			lastKey = KeyCode.A;
		} else if (Input.GetKeyDown (KeyCode.Z) && (!targetEnabled || !Input.GetKeyDown(lastKey)) && chrPath != 3) {
			startingPosition = new Vector3 (character.transform.position.x + 2.835f, -1.256f, 5f);
			SetPosition ();
			lastKey = KeyCode.Z;
		}
			
		if (targetEnabled) {
			rend.enabled = true;
			transform.position = 1.735f * Mathf.Sin (2f / period * Mathf.PI * Time.time - Mathf.PI / 2f) * Vector3.right + startingPosition;
		}
			
		//print (inSameFrame);

		if ((Input.GetKeyDown(lastKey) && targetEnabled) && !inSameFrame) {
			targetEnabled = false;
			rend.enabled = false;
			print ("targetEnabled = false");
			newTarget = true;
		}
	}

	void SetPosition() {
		transform.position = startingPosition;
		targetEnabled = true;
		inSameFrame = true;
	}
}
