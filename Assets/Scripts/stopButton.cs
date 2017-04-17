using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class stopButton : MonoBehaviour {
	public bool pause;
	public Sprite stop;
	public Sprite cont;
	public Image img;

	void Start() {
		pause = false;
		img = GetComponent<Image> ();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.P)) {
			ClickStop ();
		}
	}

	public void ClickStop() {
		pause = !pause;
		if (pause) {
			Time.timeScale = 0;
			img.sprite = cont;
		} else {
			Time.timeScale = 1f;
			img.sprite = stop;
		}
	}
}
