using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceD6_456 : DiceValue {

    public override int GetDieValue()
    {
        int defValue = die.value;
        switch (defValue)
        {
            case 1: return 6;
            case 2: return 5;
            case 3: return 4;
            default: return defValue;
        }
    }
}
