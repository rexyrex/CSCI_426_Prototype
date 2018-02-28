using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextScript : MonoBehaviour {

	public Animator animator;
	private Text damageText;

	// Use this for initialization
	void Start () {
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy (gameObject, clipInfo [0].clip.length);
		damageText = animator.GetComponent<Text> ();
	}

	public void SetText(string text){
		damageText = animator.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
