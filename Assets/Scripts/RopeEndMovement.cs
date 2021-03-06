﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEndMovement : MonoBehaviour {
	public Material mt;	//rope material (color)
	public GameObject destination;	//destination point
	public GameObject character;
	public GameObject target;
	public bool ropeReached;	//true when rope reached the target
	public bool ropeEjected;	//true when rope is ejected from the character
	public bool ropeHitting;	//true when rope hit effect on (only one frame)
	public Color startColor;
	public Color endColor;


	private Vector3 characterOffset;// allow beam shine from the bulb of the character
	private Vector3 backOffset; //allow beam to show in front of blocks
	private TargetSelection targetScript;
	private CharacterHealth health;
	private Vector3 direction;
	public LineRenderer rope;
	private float distanceFromCharacter;
	private AudioSource source;

	void Start () {
		targetScript = target.GetComponent<TargetSelection> ();
		source = GetComponent<AudioSource> ();
		health = character.GetComponent<CharacterHealth> ();
		ropeReached = false;
		characterOffset = new Vector3 (0.126f, 0.1076f, -0.1f); //(0,0.17,0.1)
		backOffset = new Vector3 (0f, 0f, -0.1f);
	}

	// Update is called once per frame
	void Update () {
		distanceFromCharacter = Vector3.Distance (character.transform.position, transform.position);
		ropeHitting = false;

		if (!ropeEjected && !ropeReached) {//rope haven't been ejected
			if (targetScript.newTarget) {//a new target is valid
				rope = gameObject.AddComponent<LineRenderer> () as LineRenderer;
				setRopePosition ();
				rope.startWidth = 0.05f;
				rope.endWidth = 0.02f;
				//rope.material = mt;
				ropeEjected = true;
				rope.material = new Material (Shader.Find ("Particles/Additive"));
				rope.startColor = startColor;
				rope.endColor = endColor;

				source.PlayOneShot (source.clip, Random.Range (0.5f, 1f));


			} else {//if there is no new target existed, follow the character
				transform.position = character.transform.position;
			}
		} else if (ropeEjected && !ropeReached ) {//rope ejected but not reached the target(destination) yet	
			direction = Vector3.Normalize (destination.transform.position - transform.position);
			transform.position = transform.position + direction * 17f * Time.deltaTime;
			setRopePosition ();
			if (Vector3.Distance (transform.position, destination.transform.position) < 0.2f) { //the end point approx.(0.2f) reached the target(destination)
				targetScript.newTarget = false; //the new target has reached, not new target any more
				ropeReached = true; //rope has reached
				ropeHitting = true;


			}
		} else if (ropeEjected && ropeReached && distanceFromCharacter >= 0.2f) {//rope ejected, reached, character not reached rope end yet
			transform.position = destination.transform.position; //move along with it's destination point(the point wherever the end reached)
			setRopePosition();
		}
	}



	void setRopePosition() {
		rope.SetPosition (0, character.transform.position + backOffset + characterOffset);//start point
		rope.SetPosition (1, transform.position + backOffset);//end point
		rope.startColor = new Color(startColor.r, startColor.g, startColor.b, health.health / 100);
		rope.endColor = new Color(endColor.r, endColor.g, endColor.b, health.health / 100);
	}
}
