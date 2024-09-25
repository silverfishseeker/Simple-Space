using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Deslizador, IDangerous
{
    public float damage = 1;

    public float danger => damage;

}
