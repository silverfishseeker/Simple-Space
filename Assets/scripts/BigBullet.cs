using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : Bullet{

    public float damageTime;
    private float nextDamage = -1;

    private void OnTriggerEnter2D(Collider2D colision) {
        IDamageable damageable = colision.gameObject.GetComponent<IDamageable>();
        if (damageable != null){
            if (nextDamage < 0) {
                damageable.Damage(damage);
                nextDamage = Time.time + damageTime;
            } else
                while (nextDamage < Time.time) {
                    damageable.Damage(damage);
                    nextDamage += damageTime;
                }
        }
    }
}
