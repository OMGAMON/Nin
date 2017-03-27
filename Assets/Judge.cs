using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour {

	private static int player1Score;
	private static int player2Score;
	private Text judgeText;		//display which player win
	private Text player1Count;	//display player1's score
	private Text player2Count;	//display player2's score
	private Canvas canvas;
	private bool died;			//true when one player dies (reaches the judge block) in this turn

	void Start () {
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
			SetText (2);	//update player 2's scoreboard
			died = true;
		}
		if (coll.gameObject.tag == "Player 2" && !died) {
			player1Score++;
			SetText (1);	//update player 1's scoreboard
			died = true;
		}

		Invoke("gameReset", 5f);	//restart the game after 5 seconds
	}

	void gameReset() {
		canvas.enabled = false;		//stop showing the scores before restarting
		SceneManager.LoadScene ("GamePlay");	//restart
	}

	void SetText(int i) {
		player1Count.text = "" + player1Score;
		player2Count.text = "" + player2Score;
		judgeText.text = "Player " + i + " won !";
		canvas.enabled = true;	//enable after all changes are done
	}
}
