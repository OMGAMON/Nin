using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour {

	public static GameCtrl control;
	public Dictionary<String, KeyCode> ctrlDict;
	public String buttonName;
	public bool waitingForInputKey;

	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
	}

	void Start () {
		ctrlDict = new Dictionary<String, KeyCode> ();
		//waitingForInputKey = false;
		load();
	}


	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/GameCtrl.dat");

		GameCtrlData data = new GameCtrlData();
		data.ctrlDict = ctrlDict;

		bf.Serialize(file, data);
		file.Close();
	}

	public void load() {
		if (File.Exists(Application.persistentDataPath + "/GameCtrl.dat")) {
			FileStream file = File.Open (Application.persistentDataPath + "/GameCtrl.dat", FileMode.Open);
			BinaryFormatter bf = new BinaryFormatter ();
			GameCtrlData data = (GameCtrlData)bf.Deserialize (file);
			file.Close ();

			ctrlDict = data.ctrlDict;
		} else {
			iniDict ();
		}
	}

	public void iniDict() {
		ctrlDict.Clear ();
		ctrlDict.Add ("p1l1", KeyCode.Q);
		ctrlDict.Add ("p1l2", KeyCode.A);
		ctrlDict.Add ("p1l3", KeyCode.Z);
		ctrlDict.Add ("p1t", KeyCode.Space);
		ctrlDict.Add ("p2l1", KeyCode.O);
		ctrlDict.Add ("p2l2", KeyCode.K);
		ctrlDict.Add ("p2l3", KeyCode.M);
		ctrlDict.Add ("p2t", KeyCode.N);
		Save ();
	}
}

[Serializable]
class GameCtrlData {
	public Dictionary<String, KeyCode> ctrlDict;
}
