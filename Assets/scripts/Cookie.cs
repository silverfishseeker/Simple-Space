using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour, IDamageable, IDangerous
{
    public float velocidad;
    private float velociadTreshhold = 0.01f;
    public float limiteInferior = -5;
    public float salud;
    public int score;
    public float dealDamage;
    public float incrVporRebote;
    public float sizeCoef;

    public float danger => dealDamage;
    private Rigidbody2D rb;

    private void Beging(){
        rb = GetComponent<Rigidbody2D>();
        float angulo = Random.Range(180, 360) * Mathf.Deg2Rad;
        Vector2 direccion = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo));
        rb.velocity = direccion * velocidad;
        UpdateOrientation();
        Debug.Log(++iii);
    }

    private static int iii = 0;

    private void UpdateOrientation() {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected void Start() {
        Beging();
    }

    private void Rebote(Vector2 normal) {
        Vector2 direccionReflejada = Vector2.Reflect(rb.velocity.normalized, normal);
        rb.velocity = direccionReflejada * rb.velocity.magnitude * incrVporRebote;
        UpdateOrientation();
    }

    private void OnCollisionEnter2D(Collision2D colision) {
        Rebote(colision.contacts[0].normal);
    }

    protected void Update() {
        if (transform.position.y < limiteInferior){
            Rebote(Vector2.up);
            transform.position = new Vector3(transform.position.x , limiteInferior + 0.001f, transform.position.z);
        }

        if (rb.velocity.magnitude < velociadTreshhold)
            rb.velocity =  velocidad * rb.velocity.normalized;
    }

    public void Copia(){
        GameObject go = Instantiate(gameObject);
        go.transform.localScale = go.transform.localScale * sizeCoef;

        Cookie goCokie = go.GetComponent<Cookie>();
        goCokie.salud -= 1;
        goCokie.Beging();
    }
    
    public void Damage(float dam){
        if (salud > 0) {
            Copia();
            Copia();
        }
        Engine.en.Score(score);
        Destroy(gameObject);
    }
}
