using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Bonus
{
    public override void bonus(Triangle tri){
        tri.numOfNormalBala+=1;
    }
}
