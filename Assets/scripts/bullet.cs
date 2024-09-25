using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public float velocidad;
    public float alturaMaxima;
    

    void Start() {
    }

    void Update() {
        if (transform.position.y < alturaMaxima)
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D colision) {
        IDamageable damageable = colision.gameObject.GetComponent<IDamageable>();
        
        if (damageable != null){
            damageable.damage(1);
            Destroy(gameObject);
        }
    }
}
