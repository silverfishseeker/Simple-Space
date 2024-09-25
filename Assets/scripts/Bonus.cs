using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : Deslizador
{
    public abstract void bonus();

    private void OnTriggerEnter2D(Collider2D colision) {
        if (colision.gameObject.GetComponent<Triangle>() != null){
            bonus();
            Destroy(gameObject);
        }
    }

}
