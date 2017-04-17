using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGUICtrl : MonoBehaviour {

	private Text judgeText;		//display which player win
	private Text player1Count;	//display player1's score
	private Text player2Count;	//display player2's score
	private Text mileage;
	private Text p1HealthBoard;
	private Text p2HealthBoard;
	private GameObject scoreBoard;
	private float deadTime;
	private Judge judge;
	private CharacterHealth p1Health;
	private CharacterHealth p2Health;
	private Image p1Bar;
	private Image p2Bar;

	private float lerpSpeed = 4f;


	// Use this for initialization
	void Start () {
		scoreBoard = GameObject.Find ("ScoreBoard");
		judgeText = GameObject.Find ("JudgeText").GetComponent<Text> ();
		player1Count = GameObject.Find ("Player1Count").GetComponent<Text> ();
		player2Count = GameObject.Find ("Player2Count").GetComponent<Text> ();
		p1Health = GameObject.Find ("Character_1").GetComponent<CharacterHealth> ();
		p2Health = GameObject.Find ("Character_2").GetComponent<CharacterHealth> ();
		mileage = GameObject.Find ("Mileage").GetComponent<Text> ();
		p1HealthBoard = GameObject.Find ("p1_health").GetComponent<Text> ();
		p2HealthBoard = GameObject.Find ("p2_health").GetComponent<Text> ();
		p1Bar = GameObject.Find ("Bar_1").GetComponent<Image> ();
		p2Bar = GameObject.Find ("Bar_2").GetComponent<Image> ();
		judge = GameObject.Find ("Judge").GetComponent<Judge> ();
		scoreBoard.SetActive(false);
	}

	void OnGUI() {
		Event e = Event.current;



		if (p1Health.hitted || p2Health.hitted) {
			p1HealthBoard.text = "" + Mathf.Round(Mathf.Lerp(p1Bar.fillAmount * 100, p1Health.health, Time.deltaTime * lerpSpeed)) + " %";
			p1Bar.fillAmount = Mathf.Lerp(p1Bar.fillAmount, p1Health.health / 100, Time.deltaTime * lerpSpeed);
			p2HealthBoard.text = "" + Mathf.Round(Mathf.Lerp(p2Bar.fillAmount * 100, p2Health.health, Time.deltaTime * lerpSpeed)) + " %";
			p2Bar.fillAmount = Mathf.Lerp(p2Bar.fillAmount, p2Health.health / 100, Time.deltaTime * lerpSpeed);
			if (Mathf.Abs (p1Bar.fillAmount * 100 - p1Health.health) < 0.1f && Mathf.Abs (p2Bar.fillAmount * 100 - p2Health.health) < 0.1f) {
				p1Health.hitted = false;
				p2Health.hitted = false;
			}
		}

		if (judge.died) {
			if (!scoreBoard.activeSelf) {
				SetRoundEnd (judge.winner, Judge.player1Score, Judge.player2Score);
			} else if (e.isKey && Input.anyKeyDown && Time.time - deadTime > 2f) {
				gameReset ();
			}
		} else {
			SetMileage (judge.meters);
		}
	}
	
	public void gameReset() {
		scoreBoard.SetActive(false);		//stop showing the scores before restarting
		SceneManager.LoadScene ("GamePlay");	//restart
	}

	public void SetMileage(float meters) {
		mileage.text = meters.ToString("F2") + " M";	//round to 2 decimals
	}

	public void SetRoundEnd(int i, int player1Score, int player2Score) {
		player1Count.text = "" + player1Score;
		player2Count.text = "" + player2Score;
		judgeText.text = "P L A Y E R  " + i + "  W O N";
		scoreBoard.SetActive(true);	//enable after all changes are done
		deadTime = Time.time;
		Invoke ("AskContinue", 2f);
	}

	public void AskContinue() {
		judgeText.text = "P R E S S  A N Y K E Y \nT O\nC O N T I N U E";
	}
}
