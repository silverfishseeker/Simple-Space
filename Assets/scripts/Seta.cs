using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta : Bonus
{
    public override void bonus(Triangle tri){
        Engine.en.poder+=1;
    }
}
