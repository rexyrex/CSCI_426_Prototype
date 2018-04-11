using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingManaTree : BreakScript {
    float maxSize = 0.5f;
    float mana = 30;
    bool grown;
    float fullSize;
    
    
    private void Awake()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
    }

    // Use this for initialization
    void Start () {
        grown = false;
        fullSize = Random.Range(0.4f, 0.7f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!grown)
        {   
            float size = this.transform.localScale.x + 0.5f * Time.deltaTime / 2.5f;
            if (size >= fullSize) grown = true;
            this.transform.localScale = new Vector3(size, size, size);
        }
	}

    // Break this object to give mana
    public override void Break()
    {
        GlobalDataController.gdc.currentMana += mana;
        Destroy(this.gameObject);
        Destroy(this);
    }
}
