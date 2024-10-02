using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadrado : HasHealth, IDangerous
{
    public int scoree;
    public float dealDamage;

    public override int score => scoree;
    public float danger => dealDamage;

}
