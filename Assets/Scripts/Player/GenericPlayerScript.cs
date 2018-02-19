using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericPlayerScript : MonoBehaviour {

    public float currentHealth;
    public float maxHealth;
    public float defaultHealth;

    public Slider manaBar;

    public Slider healthBar;


	// Use this for initialization
	void Start () {
        currentHealth = defaultHealth;
        
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        manaBar.value = GlobalDataController.gdc.currentMana/100;
        healthBar.value = currentHealth / 100;
    }


    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "SphereTag")
        {
            Damage(20);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Sphere2Tag")
        {
            Damage(10);
            Destroy(col.gameObject);
        }

		if (col.gameObject.tag == "Enemy")
		{
			Damage(10);
			Destroy(col.gameObject);
		}

        if (col.gameObject.tag == "ManaTag")
        {
            GlobalDataController.gdc.currentMana += 30;
            Destroy(col.gameObject);
        }
    }

    public virtual void Damage(float value)
    {
        
    }
}
