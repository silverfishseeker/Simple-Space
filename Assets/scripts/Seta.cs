using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta : Bonus
{
    public override void bonus(){
        Engine.en.poder+=1;
    }
}
