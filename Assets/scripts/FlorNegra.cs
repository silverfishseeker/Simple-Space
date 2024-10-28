using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorNegra : Bonus
{
    public override void bonus(Triangle tri){
        tri.BigBulletSizeCoef*=1.1f;
    }
}
