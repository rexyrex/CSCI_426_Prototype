using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Update the time slider according to time left. Attatch this directly to the slider
public class TimeLeftSliderScript : MonoBehaviour {
	Slider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = GlobalDataController.gdc.timeLeft / GlobalDataController.gdc.timeLimit;
	}
}
