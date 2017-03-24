using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationMovement : MonoBehaviour {
	public GameObject target;
	public GameObject character;

	private BoxCollider2D characterCol;
	private float blockSpeed;
	private StaticBlockMovement staticBlockScript;

	// Use this for initialization
	void Start () {
		//target = GameObject.Find ("Target");
		//character = GameObject.Find ("Character");
		characterCol = character.GetComponent<BoxCollider2D> ();
		staticBlockScript = GameObject.Find ("static block").GetComponent<StaticBlockMovement>() ;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!characterCol.enabled) {
			blockSpeed = staticBlockScript.blockSpeed; 
			transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left; //x' = x + vt
			print("newTarget = true");
		} else {
			transform.position = target.transform.position;
			print ("newTarget = false");
		}
	}
}
