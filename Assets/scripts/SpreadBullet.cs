using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet: Bullet{

    public float anguloMaximoDispersion; 
    

    void Start() {
        float angulo = Random.Range(90-anguloMaximoDispersion, 90+anguloMaximoDispersion) * Mathf.Deg2Rad;
        Vector2 direccion = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
        GetComponent<Rigidbody2D>().velocity = velocidad * direccion;
    }

    void Update() {
        if (!Engine.en.frame.IsIn(transform.position.x, transform.position.y))
            Destroy(gameObject);
    }
}
