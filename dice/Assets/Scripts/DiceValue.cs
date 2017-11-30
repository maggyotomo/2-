using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceValue : MonoBehaviour {

    public int value;
    public bool stoped;
    protected Die die;
    protected Rigidbody rigid;

	// Use this for initialization
	void Start () {
        die = gameObject.GetComponent<Die>();
        rigid = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        value = GetDieValue();
        if(gameObject.transform.position.y <= -5)
        {
            value = 0;
            stoped = true;
        }
		
	}

    void FixedUpdate()
    {
        stoped = rigid.IsSleeping();
        
    }

    public virtual int GetDieValue()
    {
        return  die.value;
    }
    public virtual int GetMaxValue()
    {
        return 6;
    }

}
