using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float blockSpeed;
	public int path;	//path number (1 represents the top lane, 2 is the middle lane, 3 is the bottom lane)
	public float[,] lane = new float[2,3]{{0.9f, -0.45f, -1.75f},{1.45f, 0.15f, -1.15f}}; //i: 1 = min, 2 = max; j: 1,2,3 corresponding lane; 1.4; 0; -1.2
	public GameObject destinationPoint;
	public GameObject ropeEnd;
	public ParticleSystem flame;
	public KeyCode thrustKey;
	public AudioClip thrustClip;

	private float distanceToSteadyX;		//character distance to steadyPoint in x-axis
	private float distanceToDestination;	//character distance to destination point
	private GameObject steadyPoint;			//the ultimate (time approach infinity) x position where the character would be after changing position
	private RopeEndMovement ropeScript;
	private BoxCollider2D col;
	private Rigidbody2D rb;
	private Vector3 direction;
	private float characterSpeed;
	private Light lt;
	private bool leave;
	private StaticBlockMovement staticBlockScript;
	public bool resetingPosition;


	private bool thrusting;
	private float thrustStartTime;
	public int thrustCount;
	private AudioSource Source;
	private Animator anim;

	private int collisionCount;

	void Start () {
		Physics2D.queriesStartInColliders = false;

		if (this.tag == "Player 1") {
			transform.position = new Vector3 (-2.45f, 1.378f, 5f); 	//player 1 initial position;
			GameCtrl.control.ctrlDict.TryGetValue("p1t", out thrustKey);
		} else {	
			transform.position = new Vector3 (-2.45f, 0.05f, 5f);	//player 2 initial position;
			GameCtrl.control.ctrlDict.TryGetValue("p2t", out thrustKey);
		}
		staticBlockScript = GameObject.Find ("static block").GetComponent<StaticBlockMovement> ();
		steadyPoint = GameObject.Find ("SteadyPoint");	//SteadyPoint is at (-2.14, 0, 0) under parent Camera
		ropeScript = ropeEnd.GetComponent<RopeEndMovement> ();
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		lt = GetComponent<Light> ();
		anim = GetComponent<Animator> ();

		Source = GetComponent<AudioSource> ();

		collisionCount = 0;
		thrusting = false;
	}

	void FixedUpdate () {
		path = Lane (transform);	//identify the lane number for target
		distanceToDestination = Vector3.Distance (destinationPoint.transform.position, transform.position); //distance to destination point
		characterSpeed = 5 + 2.5f * staticBlockScript.blockSpeed;
		anim.SetBool("ejected", ropeScript.ropeEjected);

		travelingToDestination ();
		backingAdjust ();
		thrust ();
		lightShining(1f, 0.1f, 0.3f);
	}

	void Update() {
		if (collisionCount == 0 && !ropeScript.ropeEjected && thrustCount == 0) {
			if (Input.GetKeyDown (thrustKey)) {
				flame.Play ();
				Source.PlayOneShot (thrustClip, Random.Range (0.5f, 1f));
				thrusting = true;
				thrustStartTime = Time.time;
				thrustCount++;
			}
		}
	}

	void thrust() {
		if (thrusting) {
			if (Time.time - thrustStartTime < 0.2f) {//thrust for 0.2 seconds
				transform.position = transform.position + Vector3.right * 4f * Time.fixedDeltaTime;
			} else {
				flame.Stop ();
				thrusting = false;
			}
		}
	}

	void travelingToDestination() {
		if (ropeScript.ropeReached) {// after the rope end has reached the target, start moving
			if (!leave) {//the rope end has reached the target but character haven't left the lane yet

				col.enabled = false;					 //allow character to travel through blocks without colliding on them
				rb.bodyType = RigidbodyType2D.Kinematic; //allow character to travel in straight line ignoring the physics impacts
				transform.position = transform.position + Vector3.back * 0.2f; //allow character to appear in front of blocks when transforming
				collisionCount = 0;
				leave = true; 
			} else if (distanceToDestination > 0.2f && leave) {//character left, not reaching the destination yet
				direction = Vector3.Normalize (destinationPoint.transform.position - transform.position);
				transform.position = transform.position + direction * characterSpeed * Time.fixedDeltaTime;
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
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Block") {
			if (onBlockTop (coll)) {
				thrustCount = 0;
				collisionCount++;
			}

			distanceToSteadyX = Mathf.Abs (transform.position.x - steadyPoint.transform.position.x);	//only measure once, backing speed base on the largest offset to steadyPoint
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
			if (coll.gameObject.tag == "Block") {
				if (distanceToSteadyX > 0.3f) {//has enough offset (0.1f) to adjust position, prevent blockSpeed divide number near a zero
					blockSpeed = staticBlockScript.blockSpeed;
					transform.position = transform.position + Time.fixedDeltaTime * (blockSpeed / distanceToSteadyX + 0.02f) * (transform.position.x - steadyPoint.transform.position.x) * Vector3.left;
				}
			}		
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Block") {
			if (!onBlockTop()) {
				collisionCount = 0;
			}
		}
	}

	void backingAdjust() {
			if (!ropeScript.ropeEjected) {
				if (collisionCount == 0 && (transform.position.x - steadyPoint.transform.position.x) < -0.2f) {//0.05f
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

	bool onBlockTop (Collision2D coll) {
		Vector2 direction = (coll.gameObject.transform.position - this.transform.position).normalized;
		RaycastHit2D rayHit; 
		rayHit = Physics2D.Raycast (this.transform.position, direction, 1f);
		if (rayHit != null) {
			if (rayHit.collider != null) {
				Vector2 norm = rayHit.normal;
				norm = rayHit.transform.TransformDirection (norm);
				return (norm == Vector2.up);
			}
		} 
		return false;
	}

	bool onBlockTop () {
		RaycastHit2D rayHit; 
		rayHit = Physics2D.Raycast (this.transform.position, Vector2.down, 1f);
		if (rayHit != null && rayHit.collider != null && rayHit.collider.tag == "Block") {
			Vector2 norm = rayHit.normal;
			norm = rayHit.transform.TransformDirection (norm);
			return (norm == Vector2.up);
		} 
		return false;
	}
}
