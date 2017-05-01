using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonNumberScript : MonoBehaviour {
	Button button;

	void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			int numberOfLevel = i + 1;
			transform.GetChild (i).GetComponentInChildren<Text> ().text = numberOfLevel.ToString ();
		}
	}
	
	void Update () {
	}
}
