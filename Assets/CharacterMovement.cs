using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float path;	//path number (1f represents the top lane, 2f is the middle lane, 3f is the bottom lane)
	public float blockSpeed;
	private float distance;
	private GameObject target;				//target	
	private GameObject steadyPoint;			//the ultimate (time approach infinity) x position where the character would be after changing position
	private TargetSelection targetSelection;//for fetching whether the target is a new target

	void Start () {
		target = GameObject.Find ("Target");
		steadyPoint = GameObject.Find ("SteadyPoint");
		targetSelection = target.GetComponent<TargetSelection> ();
	}

	void FixedUpdate () {
		if (transform.position.y < 1.6f && transform.position.y > 1.2f) {
			//lane 1 is when 1.2f < y < 1.6f
			path = 1f;
		} else if (transform.position.y < 0.2f && transform.position.y > -0.2f) {
			//lane 2 is when -0.2f < y < 0.2f
			path = 2f;
		} else if (transform.position.y < -1.0f && transform.position.y > -1.4f) {
			//lane 3 is when -1.4f < y < 1.0f
			path = 3f;
		}

		if (targetSelection.newTarget) {
			//the target is a new target (not transformed yet)
			transform.position = target.transform.position;
			targetSelection.newTarget = false; //set the newTarget to false so that the character would only be transform to new position one time.
			distance = transform.position.x - steadyPoint.transform.position.x;
		}


		if (transform.position.x > steadyPoint.transform.position.x) {
			blockSpeed = 5.3f * (1 - Mathf.Exp (-Time.fixedTime / 100f)) + 0.7f; 
			transform.position = transform.position + Time.fixedDeltaTime * (blockSpeed / distance + 0.2f) * (transform.position.x - steadyPoint.transform.position.x) * Vector3.left;

			// x' = x - vt, where v = x - steadyPoint;  (creates an effect of exponentially slowing down, and let the character approach steadyPoint)
			// 0.15f is the max for the character not backing on blocks. 0.7f = transform.position.x - steadyPoint.transform.position.x * 0.15f
			// 0.7f is the block speed.
		}
	}
}
