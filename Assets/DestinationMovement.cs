using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationMovement : MonoBehaviour {
	private GameObject target;
	private GameObject character;
	private BoxCollider2D characterCol;
	private float blockSpeed;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Target");
		character = GameObject.Find ("Character");
		characterCol = character.GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!characterCol.enabled) {
			blockSpeed = 5.3f * (1 - Mathf.Exp (-Time.fixedTime / 100f)) + 0.7f; 
			transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left; //x' = x + vt
			print("newTarget = true");
		} else {
			transform.position = target.transform.position;
			print ("newTarget = false");
		}
	}
}
