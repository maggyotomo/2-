using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueText : MonoBehaviour {

    public Text text;
    public Text diceCountText;
    public RollDice rollDice;

	// Use this for initialization
	void Start () {
        rollDice = GameObject.Find("DiceRoller").GetComponent<RollDice>();
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = rollDice.totalValue.ToString();
        diceCountText.text = rollDice.totalDice.ToString()+" dice";
	}
}
