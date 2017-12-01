using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueText : MonoBehaviour {

    public Text text;
    public Text eqText;
    public Text diceCountText;
    public Text IssueText;
    public Text messageText;

    public RollDice rollDice;
    public int issue1,issue2;

	// Use this for initialization
	void Start () {
        rollDice = GameObject.Find("DiceRoller").GetComponent<RollDice>();
        messageText.enabled = false;
        IssueText.text = "=10*n->d10\n" +
            ">=12->d6_456\n" + ">=50->d6_x(+n)\n";
	}
	
	// Update is called once per frame
	void Update () {
        text.text = rollDice.totalValue.ToString();
        eqText.text = "=" + rollDice.totalValue / rollDice.coeff + "x" + rollDice.coeff;
        diceCountText.text = rollDice.GetDiceCount().ToString()+"/"+rollDice.GetTotalDiceCount().ToString()+" dice";
        IssueText.text = "=10*n->d10\n" +
            ">="+issue1+"->d6_456\n" +
            ">="+issue2+"->d6_x(+n)\n";


    }

    public void ShowMessage(string message)
    {
        messageText.enabled = true;
        messageText.text = message;
    }
    

}
