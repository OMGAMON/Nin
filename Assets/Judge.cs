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
	private Text mileage;
	private float meters;
	private GameObject scoreBoard;
	private float speed;
	private bool died;			//true when one player dies (reaches the judge block) in this turn

	void Start () {
		scoreBoard = GameObject.Find ("ScoreBoard");
		judgeText = GameObject.Find ("JudgeText").GetComponent<Text> ();
		player1Count = GameObject.Find ("Player1Count").GetComponent<Text> ();
		player2Count = GameObject.Find ("Player2Count").GetComponent<Text> ();
		mileage = GameObject.Find ("Mileage").GetComponent<Text> ();

		scoreBoard.SetActive(false);
		died = false;
		meters = 0;
	}

	void Update() {
		
		if (died) {
			if (Input.anyKeyDown) {
				gameReset ();
			}
		} else {
			SetMileage ();
		}
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

		Invoke ("AskContinue", 2f);
	}

	void gameReset() {
		scoreBoard.SetActive(false);		//stop showing the scores before restarting
		SceneManager.LoadScene ("GamePlay");	//restart
	}

	void SetMileage() {
		speed = GameObject.Find ("static block").GetComponent<StaticBlockMovement> ().blockSpeed;
		meters += 2f * speed * Time.deltaTime;
		mileage.text = meters.ToString("F2") + " M";	//round to 2 decimals

	}

	void SetText(int i) {
		player1Count.text = "" + player1Score;
		player2Count.text = "" + player2Score;
		judgeText.text = "PLAYER " + i + " WON !";
		scoreBoard.SetActive(true);	//enable after all changes are done
	}

	void AskContinue() {
		judgeText.text = "PRESS ANY KEY TO CONTINUE";
	}
}
