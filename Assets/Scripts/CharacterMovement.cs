using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float blockSpeed;
	public int path;	//path number (1 represents the top lane, 2 is the middle lane, 3 is the bottom lane)
	public float[,] lane = new float[2,3]{{1.2f, -0.2f, -1.4f},{1.6f, 0.2f, -1.0f}}; //i: 1 = min, 2 = max; j: 1,2,3 corresponding lane;
	public GameObject destinationPoint;
	public GameObject ropeEnd;

	private float distanceToSteadyX;		//character distance to steadyPoint in x-axis
	private float distanceToDestination;	//character distance to destination point
	private GameObject steadyPoint;			//the ultimate (time approach infinity) x position where the character would be after changing position
	private RopeEndMovement ropeScript;
	private BoxCollider2D col;
	private Rigidbody2D rb;
	private Vector3 direction;
	private Light lt;
	private bool leave;
	private StaticBlockMovement staticBlockScript;
	private bool resetingPosition;
	public int collisionCount;

	void Start () {
		
		if (this.tag == "Player 1") {
			transform.position = new Vector3 (-2.45f, 1.378f, 5f); 	//player 1 initial position;
		} else {	
			transform.position = new Vector3 (-2.45f, 0.05f, 5f);	//player 2 initial position;
		}
		staticBlockScript = GameObject.Find ("static block").GetComponent<StaticBlockMovement> ();
		steadyPoint = GameObject.Find ("SteadyPoint");	//SteadyPoint is at (-2.14, 0, 0) under parent Camera
		ropeScript = ropeEnd.GetComponent<RopeEndMovement> ();
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		lt = GetComponent<Light> ();
		collisionCount = 0;

	}

	void FixedUpdate () {
		path = Lane (transform);	//identify the lane number for target
		distanceToDestination = Vector3.Distance (destinationPoint.transform.position, transform.position); //distance to destination point

		if (ropeScript.ropeReached) {// after the rope end has reached the target, start moving
			if (!leave) {//the rope end has reached the target but character haven't left the lane yet

				col.enabled = false;					 //allow character to travel through blocks without colliding on them
				rb.bodyType = RigidbodyType2D.Kinematic; //allow character to travel in straight line ignoring the physics impacts
				transform.position = transform.position + Vector3.back * 0.2f; //allow character to appear in front of blocks when transforming

				leave = true; 
			} else if (distanceToDestination > 0.2f && leave) {//character left, not reaching the destination yet
				direction = Vector3.Normalize (destinationPoint.transform.position - transform.position);
				transform.position = transform.position + direction * 12.5f * Time.fixedDeltaTime;
			} else if (distanceToDestination <= 0.2f && leave) {//reached the destination (allow 0.2f offset)
				col.enabled = true;						//character can collide
				rb.bodyType = RigidbodyType2D.Dynamic;	//character have physics properties
				transform.position = transform.position + Vector3.forward * 0.2f;	//return the character to original plane
				leave = false;	//character is not leaving the lane
				ropeScript.ropeEjected = false;//disable ropes when the character arrives
				ropeScript.ropeReached = false;
				Destroy (ropeScript.rope);
			}
		}

		if (!ropeScript.ropeEjected) {
			if (collisionCount == 0 && (transform.position.x - steadyPoint.transform.position.x) < -0.05f) {
				if (!resetingPosition) {
					distanceToSteadyX = Mathf.Abs (transform.position.x - steadyPoint.transform.position.x);
					resetingPosition = true;
				} else {
					blockSpeed = staticBlockScript.blockSpeed;
					transform.position = transform.position + Time.fixedDeltaTime * (blockSpeed / distanceToSteadyX + 0.02f) * (transform.position.x - steadyPoint.transform.position.x) * Vector3.left;
				}
			} else {
				resetingPosition = false;
			}
		}

		lightShining(1f, 0.1f, 0.3f);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		collisionCount++;
		if (coll.gameObject.tag == "Block") {
			distanceToSteadyX = Mathf.Abs (transform.position.x - steadyPoint.transform.position.x);	//only measure once, backing speed base on the largest offset to steadyPoint
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Block") {
			if (distanceToSteadyX > 0.1f) {//has enough offset (0.1f) to adjust position, prevent blockSpeed divide number near a zero
				blockSpeed = staticBlockScript.blockSpeed;
				transform.position = transform.position + Time.fixedDeltaTime * (blockSpeed / distanceToSteadyX + 0.02f) * (transform.position.x - steadyPoint.transform.position.x) * Vector3.left;
			}
		}		
	}

	void OnCollisionExit2D(Collision2D coll) {
		collisionCount--;
	}

	void lightShining(float period, float min, float max) { //shining with period in seconds, with min intensity, max intensity
		lt.intensity = (max - min) / 2f * Mathf.Sin (2f * Mathf.PI * Time.time / period) + (max + min) / 2f;// light
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
			return 0;//not in any lane
	}
}
