using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Judge : MonoBehaviour {

	private static int player1Score;
	private static int player2Score;
	private Text judgeText;
	private bool died;

	// Use this for initialization
	void Start () {
		player1Score = 0;
		player2Score = 0;
		judgeText = GameObject.Find ("JudgeText").GetComponent<Text> ();
		died = false;
	}
	
	// Update is called once per frame
	void Update () {
		
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
		SceneManager.LoadScene ("GamePlay");
	}

	void SetText(int i) {
		judgeText.text = "Player " + i + " won !";
	}
}
