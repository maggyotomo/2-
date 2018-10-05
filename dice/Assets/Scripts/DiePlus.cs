using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiePlus : MonoBehaviour {

	public DicePointManager dpm;

	private Button button;
	private Text dptext;

	public int cost =1000;
	public float ratio = 1.1f;
	// Use this for initialization
	void Start () {
		dpm = GameObject.Find("GameManager").GetComponent<DicePointManager>();
		button = this.GetComponent<Button>();
		button.onClick.AddListener(OnClickButton);
		button.interactable = false;
		dptext = GetComponentInChildren<Text>();
		dptext.text="1clickダイス+1\n"+cost.ToString()+"DP";
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
			float newCost = cost * ratio;
			cost =(int)newCost; 
			dptext.text="1clickダイス+1\n"+cost.ToString()+"DP";

		}
		

	}
}
