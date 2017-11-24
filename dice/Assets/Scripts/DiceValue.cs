using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValue : MonoBehaviour {

    public int value;
    private Die_d6 die;
    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        die = gameObject.GetComponent<Die_d6>();
        rigid = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        value = die.value;
		
	}
    void FixedUpdate()
    {
        if (rigid.IsSleeping())
        {
            //if (value == 0) Destroy(gameObject);
        }
        
    }
}
