using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDefault : MonoBehaviour {

	public void SetDefaultButton() {
		GameCtrl.control.iniDict();
	}
}
