using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDieRoutine : MonoBehaviour {

	private List<GameObject> diceObjs = new List<GameObject>();
	private List<GameObject> stopDice = new List<GameObject>();

	public float timeOut = 60.0f;

	public int dicePerMin = 1;
	private float timeElapsed;
	public DicePointManager dpm;
	public RollDice rolldice;

	private DiceValue diceValue;

	// Use this for initialization
	void Start () {
		dpm = GameObject.Find("GameManager").GetComponent<DicePointManager>();
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= timeOut){
			diceObjs.Add(rolldice.RollDie(0));
			timeElapsed  =0.0f;
		}
		

		foreach(GameObject diceObj in diceObjs){
			diceValue = diceObj.GetComponent<DiceValue>();
			if(diceValue.stoped){
				dpm.AddDP(diceValue.value);
				stopDice.Add(diceObj);
			}
		}
		foreach(GameObject stopdie in stopDice){
			diceObjs.Remove(stopdie);
			Destroy(stopdie, 1.0f);
		}
		
		
	}

	public void ChangeTimeOut(float t){
		timeOut = t*timeOut;
	}

	public void ChangeDicePerMin(int n){
		dicePerMin = dicePerMin + n;
		timeOut = 60.0f / dicePerMin;
	}
	

}
