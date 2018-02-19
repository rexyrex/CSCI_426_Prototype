using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Updates the time left text component
public class TimeLeftDisplayScript : MonoBehaviour {
	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Time Left : " + GlobalDataController.gdc.timeLeft;
	}
}
