using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HasHealth : Deslizador, IDamageable
{
    public float salud;
    public Color damagedColor;
    protected Color startColor;
    
    protected SpriteRenderer spriteRenderer;
    private float saludActual;

    public abstract int score{get;}

    protected void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        saludActual = salud;
        ActualizarColor();
    }

    public void Damage(float dam){
        saludActual -= dam;
        if (saludActual <= 0) {
            Engine.en.Score(score);
            Destroy(gameObject);
        }
        ActualizarColor();
    }

    protected void ActualizarColor() {
        spriteRenderer.color = Color.Lerp(damagedColor, startColor, saludActual / salud);
    }
}

