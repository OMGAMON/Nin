using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scrolling : MonoBehaviour {
	public float scrollingSpeed;
	public float tileLength = 7.834f;	//total length of one tile (the background is made up of 2 tiles)
	public float time;

	private Vector3 startingPosition;
	private GameObject staticBlock;
	private StaticBlockMovement staticBlockScript;

	void Start () {
		time = Time.timeSinceLevelLoad;
		startingPosition = transform.position;
		staticBlock = GameObject.Find ("static block");
		staticBlockScript = staticBlock.GetComponent<StaticBlockMovement> ();
	}
	
	void FixedUpdate () {
		scrollingSpeed = 0.3f * staticBlockScript.blockSpeed; 
		float newPosition = Mathf.Repeat (Time.time * scrollingSpeed, tileLength);
		transform.position = startingPosition + newPosition * Vector3.left;
	}
}
