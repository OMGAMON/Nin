using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {
	public GameObject theBlock;
	public Transform generatingPoint;
	public float speed = 0.7f;

	private float blockWidth;
	public float probability;


	// Use this for initialization
	void Start () {
		blockWidth = theBlock.GetComponent<BoxCollider2D> ().size.x;
		probability = 100f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = transform.position + Time.fixedDeltaTime * speed * Vector3.left;
		probability = 40f * Mathf.Exp (-Time.fixedTime / 200f) + 60f; 
		if (transform.position.x < generatingPoint.transform.position.x) {
			if (Random.value * 100f < probability) {
				Instantiate (theBlock, transform.position, transform.rotation);
			}
			transform.position = new Vector3 (transform.position.x + blockWidth, transform.position.y, transform.position.z);

		}
	}
}
