using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationMovement : MonoBehaviour {
	public GameObject target;
	public GameObject ropeEnd;

	private RopeEndMovement ropeEndScript;
	private float blockSpeed;
	private StaticBlockMovement staticBlockScript;

	void Start () {
		ropeEndScript = ropeEnd.GetComponent<RopeEndMovement> ();
		staticBlockScript = GameObject.Find ("static block").GetComponent<StaticBlockMovement>() ;
	}

	void Update () {
		
		if (ropeEndScript.ropeReached) {//moves with static to block whenever the rope (beam) reaches the target
			blockSpeed = staticBlockScript.blockSpeed; 
			transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left; //x' = x + vt
		} else {//move with target in otherwise
			transform.position = target.transform.position; //+ 0.1f * Vector3.up;
		}
	}
}
