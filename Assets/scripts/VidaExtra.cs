using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaExtra : Bonus
{
    public override void bonus(Triangle tri){
        tri.CambiarVida(1);
    }
}
