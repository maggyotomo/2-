using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour {

    [SerializeField]
    public List<GameObject> dice;//投げられるダイスの種類
    public Transform releasePos;
    public float speed = 100;
    public int totalValue = 0;//今の出目合計
    private int diceValue = 0;
    private int coeff = 1;

    private List<int> rollList;//ダイスの投げる順
    private int rollIndex;//何個投げたか
    private bool allStoped;//静止判定用
    private List<GameObject> diceObjs = new List<GameObject>();//場にあるダイス

    private int check_456 = 12;
    private int check_coeff = 50;

	// Use this for initialization
	void Start () {
        //diceObjs.AddRange(GameObject.FindGameObjectsWithTag("Dice"));
        rollList = new List<int> { 0, 0 };//通常ダイス2個
        rollIndex = 0;
        allStoped = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Fire1"))
        {
            if (rollIndex < rollList.Count)
            {
                RollDie(rollList[rollIndex]);
                rollIndex++;
            }
            else
            {
                allStoped = true;
            }

        }
        else if(Input.GetButtonUp("Fire2"))
        {
            StartCoroutine(RollMultiDice(rollIndex, rollList.Count));
            rollIndex = rollList.Count;
        }
        totalValue = 0;
        coeff = 1;
        foreach(GameObject diceObj in diceObjs)
        {
            diceValue = diceObj.GetComponent<DiceValue>().value;
            if (diceValue < 0)
            {
                coeff = coeff + -diceValue;
            }
            else
            {
                totalValue += diceValue;
            }
            
            if (!diceObj.GetComponent<DiceValue>().stoped) allStoped = false;
            
        }
        totalValue = coeff * totalValue;

        if (allStoped)
        {
            Debug.Log("stop_"+totalValue + "/" + GetMaxTotalValue());
            
            if (!AddSpecialDice())
            {
                rollList.Add(0);
            }
            

            ResetDice();
            allStoped = false;
            rollIndex = 0;
        }

        Debug.Log(rollIndex + "_" + totalValue);


	}

    void RollDie(int type)
    {
        GameObject die = GameObject.Instantiate(dice[type]) as GameObject;
        Vector2 mousePos = Input.mousePosition;
        Vector2 viewPos = Camera.main.ScreenToViewportPoint(mousePos);
        Vector3 force = new Vector3((viewPos.x - 0.5f) * speed, (viewPos.y - 0.5f)*speed, speed);
        //Debug.Log(force);
        die.GetComponent<Rigidbody>().AddForce(force);
        die.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(5, 10), 0, 0);
        die.transform.position = releasePos.position;
        die.transform.rotation = Random.rotation;
        diceObjs.Add(die);
    }

    void ResetDice()
    {

        foreach(GameObject obj in diceObjs)
        {
            Destroy(obj);
        }
        diceObjs.Clear();

    } 
    IEnumerator RollMultiDice(int start,int end)
    {
        for(int i = start; i < end; i++)
        {
            RollDie(rollList[i]);
            yield return new WaitForSeconds(0.2f);
        }
    }
    public int GetMaxTotalValue()
    {
        int total = 0;
        foreach(GameObject obj in diceObjs)
        {
            total += obj.GetComponent<DiceValue>().GetMaxValue();
        }
        return total;
    }

    private bool AddSpecialDice()
    {
        bool rolling = false;
        if (totalValue % 10 == 0)
        {
            rollList.Add(1);
            rolling = true;
        }
        if (totalValue >= check_456)
        {
            rollList.Add(2);
            check_456 = GetMaxTotalValue();
            rolling = true;
            Debug.Log("check_456=" + check_456);
        }
        if(totalValue >= check_coeff)
        {
            rollList.Add(3);
            check_coeff = 10 * check_coeff;
            rolling = true;
        }

        return rolling;
        
    }
}
