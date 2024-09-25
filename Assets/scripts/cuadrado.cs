using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadrado : HasHealth, IDangerous
{
    public float scoree;
    public float dealDamage;

    public override float score => scoree;
    public float danger => dealDamage;

}
