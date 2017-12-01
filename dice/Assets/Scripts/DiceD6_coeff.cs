using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceD6_coeff : DiceValue {

    public override int GetDieValue()
    {
        return -die.value;
    }

    public override int GetMaxValue()
    {
        return 0;
    }
}
