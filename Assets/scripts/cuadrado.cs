using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadrado : Deslizador, IDamageable, IDangerous
{
    public float salud;
    public float dealDamage;
    
    private SpriteRenderer spriteRenderer;
    private float saludActual;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        saludActual = salud;
        ActualizarColor();
    }

    public void damage(float dam){
        saludActual -= dam;
        if (saludActual <= 0) {
            Engine.en.Score(1);
            Destroy(gameObject);
        }
        ActualizarColor();
    }

    public float danger() => dealDamage;

    void ActualizarColor() {
        spriteRenderer.color = Color.Lerp(Color.red, Color.green, saludActual / salud);
    }
}
