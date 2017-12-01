using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diced10 : DiceValue{


    public override int GetDieValue()
    {
        return die.value;
    }

    public override int GetMaxValue()
    {
        return 9;
    }
}
