using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public int path;	//path number (1 represents the top lane, 2 is the middle lane, 3 is the bottom lane)
	public float[,] lane = new float[2,3]{{1.3f, -0.1f, -1.3f},{1.5f, 0.1f, -1.1f}}; //i: 1 = min, 2 = max; j: 1,2,3 corresponding lane;
	public float blockSpeed;
	public float force = 20f;
	//public float time;

	private float distance;
	public GameObject target;				//target	
	private GameObject steadyPoint;			//the ultimate (time approach infinity) x position where the character would be after changing position
	//private TargetSelection targetSelection;//for fetching whether the target is a new target
	public GameObject destinationPoint;
	public GameObject ropeEnd;
	private RopeEndMovement ropeScript;
	private int targetPath;
	private BoxCollider2D col;
	private Rigidbody2D rb;
	private Vector3 offset;
	private Light lt;
	private bool leave;
	private GameObject staticBlock;
	private StaticBlockMovement staticBlockScript;

	void Start () {
		//time = Time.timeSinceLevelLoad;
		staticBlock = GameObject.Find ("static block");
		staticBlockScript = staticBlock.GetComponent<StaticBlockMovement> ();
		steadyPoint = GameObject.Find ("SteadyPoint");
		//destinationPoint = GameObject.Find ("DestinationPoint");
		ropeScript = ropeEnd.GetComponent<RopeEndMovement> ();
		//targetSelection = target.GetComponent<TargetSelection> ();
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		lt = GetComponent<Light> ();

	}

	void FixedUpdate () {
		path = Lane (transform);

		if (ropeScript.ropeReached && !leave) {
			//the target is a new target (not transformed yet)
			targetPath = Lane (target.transform);

			col.enabled = false;
			rb.bodyType = RigidbodyType2D.Kinematic;

			leave = true; //set leave to true so that the character would only be transform to new position one time.
		}

		if (Lane (transform) != targetPath && !col.enabled) {
			offset = Vector3.Normalize(destinationPoint.transform.position - transform.position);
			transform.position = transform.position + (offset) * 7.0f * Time.fixedDeltaTime;
		} else if (Lane(transform) == targetPath && !col.enabled) { //Destroy (spring);
			col.enabled = true;
			rb.bodyType = RigidbodyType2D.Dynamic;
			distance = transform.position.x - steadyPoint.transform.position.x;
			leave = false;
		}

		if (transform.position.x != steadyPoint.transform.position.x && col.enabled) {
			blockSpeed = staticBlockScript.blockSpeed;
			transform.position = transform.position + Time.fixedDeltaTime * (blockSpeed / distance + 0.02f) * (transform.position.x - steadyPoint.transform.position.x) * Vector3.left;
			// x' = x - vt, where v = x - steadyPoint;  (creates an effect of exponentially slowing down, and let the character approach steadyPoint)
			// 0.15f is the max for the character not backing on blocks. 0.7f = transform.position.x - steadyPoint.transform.position.x * 0.15f
			// 0.7f is the block speed.
		}

		lt.intensity = 0.15f * Mathf.Sin (2f * Mathf.PI * Time.time) + 0.25f;// light
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
