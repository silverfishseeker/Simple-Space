using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : HasHealth, IDangerous
{
    public float scoree;
    public float dealDamage;

    public override float score => scoree;
    public float danger => dealDamage;

    public float goStraightHeight;
    public float xVelocidad;
    public float periodo;
    public float cadencia;
    public float tiempoDisparo;
    public Color shootColor;
    public GameObject balas;

    private float nextTime;
    private bool notShooting;
    private Color actualStartColor;

    new void Start(){
        base.Start();
        nextTime = Time.time + cadencia;
        notShooting = true;
        actualStartColor = startColor;
    }

    new void Update(){
        base.Update();
        
        if (notShooting && transform.position.y > goStraightHeight) {
            float num = Mathf.Sin(transform.position.y * periodo);
            transform.Translate(Vector3.right * num * xVelocidad * Time.deltaTime);
        }else{
            float razonTiempoDisparando = (nextTime - Time.time) / tiempoDisparo;
            startColor = Color.Lerp(actualStartColor, shootColor, razonTiempoDisparando);
            ActualizarColor();
        }
        
        while (nextTime < Time.time) {

            if (notShooting) {
                notShooting = false;
                spriteRenderer.color = shootColor;
                nextTime += tiempoDisparo;
            }else{
                notShooting = true;
                Instantiate(balas, transform.position, Quaternion.identity);
                nextTime += Random.Range(0.01f, cadencia);
                startColor = actualStartColor;
                ActualizarColor();
            }
        }
    }

}