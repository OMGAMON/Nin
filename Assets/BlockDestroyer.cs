using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour {
	public GameObject destructionPoint;


	// Use this for initialization
	void Start () {
		destructionPoint = GameObject.Find ("DestroyingPoint");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x < destructionPoint.transform.position.x) {
			Destroy (gameObject);		
		}
		
	}
}
