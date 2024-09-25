using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : Deslizador
{
    public abstract void bonus(Triangle tri);

    private void OnTriggerEnter2D(Collider2D colision) {
        Triangle tri = colision.gameObject.GetComponent<Triangle>();
        if (tri != null){
            bonus(tri);
            Destroy(gameObject);
        }
    }

}
