using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuadrado : MonoBehaviour, IDamageable, IDangerous
{
    public float velocidad;
    public float limiteInferior;
    public float salud;
    public float dealDamage;
    
    private SpriteRenderer spriteRenderer;
    private float saludActual;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        saludActual = salud;
        ActualizarColor();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);

        if (transform.position.y < limiteInferior)
            Destroy(gameObject);
    }

    public void damage(float dam){
        saludActual -= dam;
        if (saludActual <= 0)
            Destroy(gameObject);
        ActualizarColor();
    }

    public float danger() => dealDamage;

    void ActualizarColor() {
        spriteRenderer.color = Color.Lerp(Color.red, Color.green, saludActual / salud);
    }
}
