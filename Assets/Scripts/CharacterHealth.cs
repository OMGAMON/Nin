using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {
	public float health;
	public ParticleSystem explosion;
	public AudioClip explosionClip;
	public bool hitted;

	private Judge judge;
	private SpriteRenderer rend;
	private bool died;
	private float deductionShooting = 60f;
	private AudioSource source;


	// Use this for initialization
	void Start () {
		judge = GameObject.Find ("Judge").GetComponent<Judge>();
		rend = GetComponent<SpriteRenderer> ();
		source = GetComponent<AudioSource> ();
		health = 100f;
		died = false;
		hitted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (health < 0) {
			health = 0;
		}

		if (health <= 0 && !died) {
			died = true;
			if (this.tag == "Player 1") {
				judge.player2Win ();
			} else {				
				judge.player1Win ();
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "RopeEnd") {
			RopeEndMovement ropeEndScript = other.gameObject.GetComponent<RopeEndMovement> ();
			if (ropeEndScript.ropeHitting) {
				hitted = true;
				health -= deductionShooting;//-20f
				source.PlayOneShot(explosionClip, 3f);
				rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, health/100);//0.15
				explosion.Play ();
				Invoke ("expStop", 1f);

			}
		}
	}
	void expStop() {
		explosion.Stop ();
	}
}
