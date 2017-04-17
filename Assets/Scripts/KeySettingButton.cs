using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeySettingButton : MonoBehaviour {
	public static Dictionary<string, bool> selected;
	public static bool waitingForInputKey;
	public Text keyText;
	public bool ifSelected;
	private KeyCode key;
	private AudioSource source;

	void Awake() {
		selected = new Dictionary<string, bool> ();
		waitingForInputKey = false;
		iniSelected ();
		source = GetComponent<AudioSource> ();
	}

	void OnGUI() {
		GameCtrl.control.ctrlDict.TryGetValue (gameObject.name, out key);
		keyText.text = key.ToString ();
		selected.TryGetValue (gameObject.name, out ifSelected);
		if (!waitingForInputKey || !ifSelected) {
			keyText.color = Color.white;
		}
			
		Event e = Event.current;
		if (waitingForInputKey) {
			if (e.isKey && Input.anyKeyDown && ifSelected) {
				string keyPressed = Input.inputString.Substring (0, 1).ToUpper ();
				waitingForInputKey = false;
				GameCtrl.control.ctrlDict.Remove (gameObject.name);
				GameCtrl.control.ctrlDict.Add (gameObject.name, (KeyCode)System.Enum.Parse (typeof(KeyCode), keyPressed));
				GameCtrl.control.Save ();
				return;
			}
		}
	}

	public void mouseDown() {
		source.PlayOneShot (source.clip, 0.5f);
		keyText.color = new Color (0, 249, 219, 255);
		selected.Clear ();
		iniSelected ();
		selected.Remove (gameObject.name);
		selected.Add (gameObject.name, true);
		//GameCtrl.control.buttonName = gameObject.name;
		//GameCtrl.control.waitingForInputKey = true;
		waitingForInputKey = true;
	}

	void iniSelected() {
		selected.Add ("p1l1", false);
		selected.Add ("p1l2", false);
		selected.Add ("p1l3", false);
		selected.Add ("p1t", false);
		selected.Add ("p2l1", false);
		selected.Add ("p2l2", false);
		selected.Add ("p2l3", false);
		selected.Add ("p2t", false);
	}
}
