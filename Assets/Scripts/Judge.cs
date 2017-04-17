using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour {

	public static int player1Score;
	public static int player2Score;
	//private static float highScore;
	public float meters;
	public int winner;

	public GameObject p1T;
	public GameObject p1D;
	public GameObject p1CR;
	public GameObject p1RP;

	public GameObject p2T;
	public GameObject p2D;
	public GameObject p2CR;
	public GameObject p2RP;

	private float speed;
	public bool died;			//true when one player dies (reaches the judge block) in this turn

	void Start () {
		died = false;
		meters = 0;
	}

	void Update() {
		speed = GameObject.Find ("static block").GetComponent<StaticBlockMovement> ().blockSpeed;
		meters += 2f * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player 1" && !died) {
			player2Win ();
		}
		if (coll.gameObject.tag == "Player 2" && !died) {
			player1Win ();
		}
	}

	public void player1Win() {
		player1Score++;
		winner = 1;
		died = true;
		p2T.SetActive (false);
		p2D.SetActive (false);
		p2RP.SetActive (false);
		p2CR.GetComponent<SpriteRenderer> ().enabled = false;
		p2CR.GetComponent<BoxCollider2D> ().enabled = false;
	}

	public void player2Win() {
		player2Score++;
		winner = 2;
		died = true;
		p1T.SetActive (false);
		p1D.SetActive (false);
		p1RP.SetActive (false);
		p1CR.GetComponent<SpriteRenderer> ().enabled = false;
		p1CR.GetComponent<BoxCollider2D> ().enabled = false;
	}
}
