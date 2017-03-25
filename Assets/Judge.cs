using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour {

	private static int player1Score;
	private static int player2Score;
	private Text judgeText;
	private Text player1Count;
	private Text player2Count;
	private Canvas canvas;
	private bool died;

	// Use this for initialization
	void Start () {
		//player1Score = 0;
		//player2Score = 0;

		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();
		judgeText = GameObject.Find ("JudgeText").GetComponent<Text> ();
		player1Count = GameObject.Find ("Player1Count").GetComponent<Text> ();
		player2Count = GameObject.Find ("Player2Count").GetComponent<Text> ();

		canvas.enabled = false;
		died = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player 1" && !died) {
			player2Score++;
			SetText (2);
			died = true;
		}
		if (coll.gameObject.tag == "Player 2" && !died) {
			player1Score++;
			SetText (1);
			died = true;
		}

		//StartCoroutine (gameReset());
		Invoke("gameReset", 5f);
	}

	void gameReset() {
		canvas.enabled = false;
		//SceneManager.UnloadSceneAsync ("Gameplay");
		SceneManager.LoadScene ("GamePlay");
	}

	void SetText(int i) {
		player1Count.text = "" + player1Score;
		player2Count.text = "" + player2Score;
		judgeText.text = "Player " + i + " won !";
		canvas.enabled = true;
	}
}
