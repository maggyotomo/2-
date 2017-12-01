using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour {
    public GameObject dice;
    public Transform releasePos;
    public float speed = 100;
    public int maxDice = 10;
    public int totalValue = 0;
    public int totalDice = 0;

    private int diceValue = 0;
    private List<GameObject> diceObjs = new List<GameObject>();

	// Use this for initialization
	void Start () {
        diceObjs.AddRange(GameObject.FindGameObjectsWithTag("Dice"));
        totalDice = diceObjs.Count;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonUp("Fire1"))
        {
            if(totalDice < maxDice)
            {
                RollDie();

            }
            else
            {
                foreach(GameObject obj in diceObjs)
                {
                    StartCoroutine(LateDestroy(0.5f, obj));
                }
            }
            
            
        }
        totalValue = 0;
        int zdice = 0;
        foreach(GameObject diceObj in diceObjs)
        {
            diceValue = diceObj.GetComponent<DiceValue>().value;
            totalValue += diceValue;
            if (diceValue == 0) zdice++;
        }
        Debug.Log(zdice);

		
	}

    void RollDie()
    {
        GameObject die = GameObject.Instantiate(dice) as GameObject;
        Vector2 mousePos = Input.mousePosition;
        Vector2 viewPos = Camera.main.ScreenToViewportPoint(mousePos);
        Vector3 force = new Vector3((viewPos.x - 0.5f) * speed, (viewPos.y - 0.5f)*speed, speed);
        //Debug.Log(force);
        die.GetComponent<Rigidbody>().AddForce(force);
        die.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(5, 10), 0, 0);
        die.transform.position = releasePos.position;
        die.transform.rotation = Random.rotation;
        diceObjs.Add(die);
        totalDice++;
    }

    IEnumerator LateDestroy(float t,GameObject obj)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
        diceObjs.Clear();
        totalDice = 0;
    }
}
