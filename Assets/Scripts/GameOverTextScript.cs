using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Alter the gameover screen's text. Attatch to the text gameobject
public class GameOverTextScript : MonoBehaviour {
	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "GAME OVER! YOU HAD " + GlobalDataController.gdc.timeLeft + " seconds left!";
	}
}
