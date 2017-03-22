using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMovement : MonoBehaviour {
	public float blockSpeed = 0.7f;
	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left;
	}
}
