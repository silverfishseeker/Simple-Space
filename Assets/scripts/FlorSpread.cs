using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorSpread : Bonus
{
    public override void bonus(Triangle tri){
        tri.numOfSpreadBala+=1;
    }
}
