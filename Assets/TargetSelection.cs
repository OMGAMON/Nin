using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {
	public GameObject character;	//character
	public float period;		//the swing period of the target (in seconds)
	public bool newTarget;			//true if the target is once disabled and the character hasn't moved to the target's position.
	public KeyCode lane1;
	public KeyCode lane2;
	public KeyCode lane3;

	private GameObject targetEndPoint;	//end point of the character's swing
	private Vector3 startingPosition;	//mid point of the swing (character's x + 1.1 + 1/2 the distance between end point and character)
	private Renderer rend;				//target's renderer
	private int chrPath;				//character's path # (either 1f, 2f, or 3f)
	private float distance;				//distance between endpoint's x and character's x
	private CharacterMovement characterMovement;	//for fetching character's path #
	private KeyCode lastKey;	//the last key hitted
	private bool targetEnabled; //true when target is enabled to be rendered and moves sinusoidally.
	private bool inSameFrame;	//true if target is enabled in a specific frame. Prevent triggering disabling target

	//					|----distance (Moving Area) ----|
	//					|								|
	//		character	|		startingPosition	targetEndPoint
	//			|		|				|				|
	//		===========================(+)=====================
	//			|
	//			|
	//			C 
	//		===================================================
	//
	//		sinusoidal equation: A * sin ( 2 pi / T * t); A is amplitude, T is period in seconds (the time needed for the target to complete 1 swing);


	void Start () {
		/*character = GameObject.Find ("Character");*/
		targetEndPoint = GameObject.Find ("TargetEndPoint");
		characterMovement = character.GetComponent<CharacterMovement> ();
		rend = GetComponent<Renderer>();
		period = 1;
		rend.enabled = false;
		newTarget = false;
	}

	void Update () {
		chrPath = characterMovement.path;
		distance = targetEndPoint.transform.position.x - character.transform.position.x;

		if (Input.GetKeyDown(lane1) && (!targetEnabled || !Input.GetKeyDown(lastKey)) && chrPath != 1) { 
			//key hitted is Q, and path is not 1, and either target is enabled or the key hitted just now is not the same as lastkey
			startingPosition = new Vector3 (character.transform.position.x + 1.1f + 0.5f * (distance - 1.1f), 1.355f, 5f);
			SetPosition ();
			lastKey = lane1;
		} else if (Input.GetKeyDown(lane2) && (!targetEnabled || !Input.GetKeyDown(lastKey)) && chrPath != 2) {
			//key hitted is A, and path is not 2, and either target is enabled or the key hitted just now is not the same as lastkey
			startingPosition = new Vector3 (character.transform.position.x + 1.1f + 0.5f * (distance - 1.1f), 0f, 5f);
			SetPosition ();
			lastKey = lane2;
		} else if (Input.GetKeyDown (lane3) && (!targetEnabled || !Input.GetKeyDown(lastKey)) && chrPath != 3) {
			//key hitted is Z, and path is not 3, and either target is enabled or the key hitted just now is not the same as lastkey
			startingPosition = new Vector3 (character.transform.position.x + 1.1f + 0.5f * (distance - 1.1f), -1.256f, 5f);
			SetPosition ();
			lastKey = lane3;
		}
			
		if (chrPath == 0) {
			targetEnabled = false;
			rend.enabled = false;
		}

		if (targetEnabled) {
			//enabling the target renderer and start moving the target
			rend.enabled = true;
			startingPosition = new Vector3 (character.transform.position.x + 1.1f + 0.5f * (distance - 1.1f), startingPosition.y, 5f); //reset mid point due to character's move
			transform.position = 0.5f * (distance - 1.1f) * Mathf.Sin (2f / period * Mathf.PI * Time.time - Mathf.PI / 2f) * Vector3.right + startingPosition;
		}
			
		/*print (inSameFrame);*/

		if ((Input.GetKeyDown(lastKey) && targetEnabled) && !inSameFrame) {
			//the key hitted just now is the same the last key, and the target is enabled, it is not in the same frame with enabling target
			targetEnabled = false;
			rend.enabled = false;
			newTarget = true;
		}

		inSameFrame = false;
	}

	//set the target's position to the starting position, enable the target, and set inSameFrame to true so that disabling target cannot happen in the current frame
	void SetPosition() {
		transform.position = startingPosition;
		targetEnabled = true;
		inSameFrame = true;
	}
}
