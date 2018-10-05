using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollDice : MonoBehaviour {

    [SerializeField]
    public List<GameObject> dice;//投げられるダイスの種類
    public Transform releasePos;
    public float speed = 100;

    public int totalValue = 0;//今の出目合計
    private DiceValue diceValue;//各ダイスの出目情報
    public int totalBaseValue = 0;//数ダイス合計
    public int coeff = 1;//倍率合計

    private List<int> rollList;//ダイスの投げる順
    private int rollIndex;//何個投げたか
    private bool allStoped;//静止判定用
    private List<GameObject> diceObjs = new List<GameObject>();//場にあるダイス

    private List<GameObject> stopDice = new List<GameObject>();
    public int check_456 = 12;//456目標値
    private int check_coeff = 50;//倍ダイス目標値

    public ValueText valTxt;

    public DicePointManager dpm;

	// Use this for initialization
	void Start () {
        dpm = GameObject.Find("GameManager").GetComponent<DicePointManager>();
        //diceObjs.AddRange(GameObject.FindGameObjectsWithTag("Dice"));
        rollList = new List<int> { 1,1};//通常ダイス2個
        rollIndex = 0;
        allStoped = false;

        valTxt.issue1 = check_456;
        valTxt.issue2 = check_coeff;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Fire1"))
        {
            StartCoroutine(RollMultiDice(rollIndex, rollList.Count));
            rollIndex = rollList.Count;
        }

        foreach(GameObject diceObj in diceObjs)
        {
            diceValue = diceObj.GetComponent<DiceValue>();
            
            if (diceValue.stoped){
                dpm.AddDP(diceValue.value);
                stopDice.Add(diceObj);
            }
            
        }
        foreach(GameObject stopDie in stopDice){
            diceObjs.Remove(stopDie);
            Destroy(stopDie,1.0f);
        }

	}

    public GameObject RollDie(int type)
    {
        GameObject die = GameObject.Instantiate(dice[type]) as GameObject;
        Vector2 mousePos = Input.mousePosition;
        Vector2 viewPos = Camera.main.ScreenToViewportPoint(mousePos);
        Vector3 force = new Vector3((viewPos.x - 0.5f) * speed, (viewPos.y - 0.5f)*speed, speed);
        die.GetComponent<Rigidbody>().AddForce(force);
        die.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(5, 10), 0, 0);
        die.transform.position = releasePos.position;
        die.transform.rotation = Random.rotation;

        return die;
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
            diceObjs.Add(RollDie(rollList[i]));
            yield return new WaitForSeconds(0.2f);
        }
        rollIndex =0;
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
            valTxt.ShowMessage("add d10");
            rolling = true;
        }
        if (totalValue >= check_456)
        {
            rollList.Add(2);
            check_456 = GetMaxTotalValue();
            rolling = true;
            valTxt.ShowMessage("add d6_456");
            valTxt.issue1 = check_456;
        }
        if(totalValue >= check_coeff)
        {
            rollList.Add(3);
            check_coeff += 50 *rollList.Count(n=>n==3)*3;
            valTxt.ShowMessage("add d6_x(+n)");
            rolling = true;
            valTxt.issue2 = check_coeff;
        }

        return rolling;
        
    }
    public int GetDiceCount()
    {
        return diceObjs.Count;

    }
    public int GetTotalDiceCount()
    {
        return rollList.Count;
    }

    public void AddRollList(int n){
            rollList.Add(n);
    }
}
