using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour {
	public GameObject destructionPoint;

	void Start () {
		destructionPoint = GameObject.Find ("DestroyingPoint");	//fetch destruction point
	}

	void FixedUpdate () {
		if (transform.position.x < destructionPoint.transform.position.x) {
			//destroy this block when it travels over the destruction point
			Destroy (gameObject);		
		}
		
	}
}
