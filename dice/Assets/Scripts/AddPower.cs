using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPower : MonoBehaviour {

	public DicePointManager dpm;

	private Button button;
	private Text dptext;

	public int cost =10000;
	public float ratio = 2.5f;
	// Use this for initialization
	void Start () {
		dpm = GameObject.Find("GameManager").GetComponent<DicePointManager>();
		button = this.GetComponent<Button>();
		button.onClick.AddListener(OnClickButton);
		button.interactable = false;
		dptext = GetComponentInChildren<Text>();
		dptext.text="DP倍率+1倍\n"+cost.ToString()+"DP";
	}
	
	// Update is called once per frame
	void Update () {
		if(!button.interactable){
			if(dpm.GetDP()>=cost){
				button.interactable = true;
			}
		}
		else{
			if(dpm.GetDP()<cost){
				button.interactable = false;
			}
		}
	}
	public void OnClickButton(){
		if(dpm.GetDP()>=cost&&button.interactable){
			dpm.UseDP(cost);
			//dpm.AddPower(1);
			float newCost = cost * ratio;
			cost =(int)newCost; 
			dptext.text="DP倍率+1倍\n"+cost.ToString()+"DP";

		}
		

	}
}
