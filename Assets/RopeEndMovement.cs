using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEndMovement : MonoBehaviour {
	public float[,] lane = new float[2,3]{{1.3f, -0.1f, -1.3f},{1.5f, 0.1f, -1.1f}};
	public bool ropeReached;
	public bool ropeEjected;
	public Material mt;

	private GameObject character;
	private BoxCollider2D characterCol;
	private GameObject target;
	private TargetSelection targetScript;
	private GameObject destination;
	private Vector3 direction;
	private LineRenderer rope;

	void Start () {
		character = GameObject.Find ("Character");
		characterCol = character.GetComponent<BoxCollider2D> ();
		target = GameObject.Find ("Target");
		targetScript = target.GetComponent<TargetSelection> ();
		destination = GameObject.Find ("DestinationPoint");
		ropeReached = false;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (targetScript.newTarget) {
			if (!ropeEjected) {//instantiate
				rope = gameObject.AddComponent<LineRenderer>() as LineRenderer;
				rope.SetPosition (0, transform.position);
				rope.SetPosition (1, character.transform.position);
				rope.startWidth = 0.05f;
				rope.endWidth = 0.02f;
				rope.material = mt;
				ropeEjected = true;
			}

			direction = Vector3.Normalize (target.transform.position - transform.position);
			transform.position = transform.position + direction * 10f * Time.deltaTime;
			if (Lane (transform) == Lane (target.transform)) {
				targetScript.newTarget = false;
				ropeReached = true;
			}
		}

		if(!ropeEjected){
			transform.position = character.transform.position;
		} else { //change rope distance;
			if (ropeReached) {
				transform.position = destination.transform.position;
			}
			rope.SetPosition (1, transform.position + Vector3.back * 0.1f);
			rope.SetPosition (0, character.transform.position + Vector3.back * 0.1f + Vector3.up * 0.17f);
		}

		if (characterCol.enabled && Lane(character.transform) == Lane(transform)) {
			ropeEjected = false;
			ropeReached = false;
			Destroy (rope);
			//destroy rope
		}

	}

	int Lane (Transform t) {
		if (t.position.y < lane [1,0] && t.position.y > lane [0,0]) {
			//lane 1 is when 1.2f < y < 1.6f
			return 1;
		} else if (t.position.y < lane [1,1] && t.position.y > lane [0,1]) {
			//lane 2 is when -0.2f < y < 0.2f
			return 2;
		} else if (t.position.y < lane [1,2] && t.position.y > lane [0,2]) {
			//lane 3 is when -1.4f < y < -1.0f
			return 3;
		} else
			return 0;
	}
}
