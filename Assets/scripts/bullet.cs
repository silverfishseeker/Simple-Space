using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public float velocidad;
    public float damage = 1;
    

    void Start() {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * velocidad;
    }

    void Update() {
        if (transform.position.y > Engine.en.alturaMaxima)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D colision) {
        IDamageable damageable = colision.gameObject.GetComponent<IDamageable>(); 
        if (damageable != null){
            damageable.Damage(damage);
            Destroy(gameObject);
        }
    }
}
