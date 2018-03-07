using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour {

	private static DamageTextScript damageText;
	private static GameObject canvas;

	public static void Initialize(){
		canvas = GameObject.Find ("DamageCanvas");
		if(!damageText){
			damageText = Resources.Load<DamageTextScript> ("Prefabs/DamageText/DamageTextParent");
			Debug.Log ("loading");
		}
		Debug.Log ("INIT");
	}

	public static void CreateFloatingText(string text, Transform location){
		if (damageText == null) {
			Debug.Log ("Expect a null");
		}
		DamageTextScript instance = Instantiate (damageText);

		Vector2 screenPosition = Camera.main.WorldToScreenPoint (location.position);
		if (instance == null) {
			Debug.Log ("Null damage text instance");
		}
		instance.transform.SetParent (canvas.transform, false);
		instance.transform.position = screenPosition;
		instance.SetText (text);
	}
}
