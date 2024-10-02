using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : HasHealth, IDangerous
{
    public int scoree;
    public float dealDamage;

    public float velocidadRotacion; 

    public override int score => scoree;
    public float danger => dealDamage;
    
    new void Update() {
        base.Update();
        transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
    }
}
