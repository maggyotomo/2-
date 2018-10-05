using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPText : MonoBehaviour {


	public DicePointManager dpm;

	private Text dpText;
	// Use this for initialization
	void Start () {
		dpm = GameObject.Find("GameManager").GetComponent<DicePointManager>();
		dpm.GetDP();
		
		this.dpText = this.GetComponent<Text>(); 
        this.dpText.text = dpm.GetDP().ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		this.dpText.text = dpm.GetDP().ToString();
	}
}
