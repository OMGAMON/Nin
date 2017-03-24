using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {
	public GameObject theBlock;
	public Transform generatingPoint;
	public float speed;
	public float maxProbability;
	public float minProbability;

	private float blockWidth;	
	public float probability;	//probability of generating a block


	void Start () {
		blockWidth = theBlock.GetComponent<BoxCollider2D> ().size.x;	//fetch block's width
		probability = 100f;
		minProbability = 40f;
		maxProbability = 70f;
	}
	
	void FixedUpdate () {
		speed = 5.3f * (1 - Mathf.Exp (-Time.fixedTime / 100f)) + 0.7f; 
		transform.position = transform.position + Time.fixedDeltaTime * speed * Vector3.left; //allow generator to move to the left with the same speed of other blocks
		probability = (maxProbability - minProbability) * Mathf.Exp (-Time.fixedTime / 200f) + minProbability; 
		//model: A * e ^ (-t / T) + B;	when time = 0, probability = A + B; when t = T, probability = 0.37A + B;
		if (transform.position.x < generatingPoint.transform.position.x) {
			if (Random.value * 100f < probability) {
				//generate a block when the random value is less than probability
				Instantiate (theBlock, transform.position, transform.rotation);
			}
			transform.position = new Vector3 (transform.position.x + blockWidth + 0.19f, transform.position.y, transform.position.z); //place the generator back a distance of a blockwidth

		}
	}
}
