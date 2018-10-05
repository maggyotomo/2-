using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePointManager : MonoBehaviour {

	[SerializeField]
	private int DicePoint;

	public float power = 1.0f;


	// Use this for initialization
	void Start () {
		DicePoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetDP(){
		return DicePoint;

	}

	public void AddDP(int d){
		float div =d*power;
		DicePoint = DicePoint+(int)div;
	}
	public void UseDP(int d){
		if(DicePoint-d>=0){
			DicePoint = DicePoint - d;
		}else{
			DicePoint = 0;
		}
		
	}
	public void AddPower(float n){
		power = power + n;
	}
}
