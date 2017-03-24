using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMovement : MonoBehaviour {
	public float blockSpeed;
	private GameObject staticBlock;
	private StaticBlockMovement staticBlockScript;

	void Start() {
		staticBlock = GameObject.Find ("static block");
		staticBlockScript = staticBlock.GetComponent<StaticBlockMovement> ();
	}

	void FixedUpdate () {
		blockSpeed = staticBlockScript.blockSpeed; 
		transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left; //x' = x + vt
	}
}
