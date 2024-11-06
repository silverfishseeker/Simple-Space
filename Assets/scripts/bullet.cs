using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public float velocidad;
    public float damage = 1;

    private static HashSet<IDamageable> currentFrameCollided = new HashSet<IDamageable>();
    

    void Start() {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * velocidad;
    }

    void Update() {
        if (transform.position.y > Engine.en.alturaMaxima)
            Destroy(gameObject);
        currentFrameCollided.Clear();
    }

    private void OnTriggerEnter2D(Collider2D colision) {
        IDamageable damageable = colision.gameObject.GetComponent<IDamageable>(); 
        if (damageable != null && !currentFrameCollided.Contains(damageable)){
            currentFrameCollided.Add(damageable);
            damageable.Damage(damage);
            Destroy(gameObject);
            Debug.Log(++iii);
        }
    }

    private static int iii = 0;
}
