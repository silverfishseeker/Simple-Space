using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deslizador : MonoBehaviour
{
    public float velocidad;
    public float limiteInferior = -5;

    protected void Start(){
        GetComponent<Rigidbody2D>().velocity = Vector2.down * velocidad;
    }

    protected void Update() {
        if (transform.position.y < limiteInferior)
            Destroy(gameObject);
    }
}
