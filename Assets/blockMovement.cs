using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMovement : MonoBehaviour {
	public float blockSpeed;

	void FixedUpdate () {
		blockSpeed = 5.3f * (1 - Mathf.Exp (-Time.fixedTime / 100f)) + 0.7f; 
		transform.position = transform.position + Time.fixedDeltaTime * blockSpeed * Vector3.left; //x' = x + vt
	}
}
