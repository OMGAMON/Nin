using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {
	public GameObject theBlock;
	public Transform generatingPoint;
	public float speed;
	public float maxProbability;
	public float minProbability;
	public float referenceTime;
	public float backOffset;

	private float blockWidth;	
	public float probability;	//probability of generating a block
	private GameObject staticBlock;
	private StaticBlockMovement staticBlockScript;

	void Start () {
		staticBlock = GameObject.Find ("static block");
		staticBlockScript = staticBlock.GetComponent<StaticBlockMovement> ();
		blockWidth = 0.9f;	//fetch block's width
		probability = 100f;
		minProbability = 40f;
		maxProbability = 70f;
		referenceTime = Time.time;
		backOffset = 0;
	}
	
	void FixedUpdate () {
		speed = staticBlockScript.blockSpeed;
		transform.position = transform.position + Time.fixedDeltaTime * speed * Vector3.left; //allow generator to move to the left with the same speed of other blocks
		probability = (maxProbability - minProbability) * Mathf.Exp (-(Time.time - referenceTime) / 200f) + minProbability; 
		//model: A * e ^ (-t / T) + B;	when time = 0, probability = A + B; when t = T, probability = 0.37A + B;
		if (transform.position.x < generatingPoint.transform.position.x) {
			if (Random.value * 100f < probability) {
				//generate a block when the random value is less than probability
				Instantiate (theBlock, transform.position, transform.rotation);
				backOffset = blockWidth;
			} else {
				backOffset = speed * 0.2f;
			}
			transform.position = new Vector3 (transform.position.x + backOffset, transform.position.y, transform.position.z); //place the generator back a distance of a blockwidth

		}
	}
}
