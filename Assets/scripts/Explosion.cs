using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float daño;
    public float duracionAnimacion;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 3 / duracionAnimacion;

        StartCoroutine(ScaleCollisionBox());
        StartCoroutine(DesactivarExplosion());
    }

    private IEnumerator ScaleCollisionBox() {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        for (float t = 0; t < duracionAnimacion; t += Time.deltaTime) {
            collider.radius = t / duracionAnimacion;
            yield return null;
        }
        collider.radius = 1;
    }

    private IEnumerator DesactivarExplosion() {
        yield return new WaitForSeconds(duracionAnimacion);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D colision) {
        IDamageable damageable = colision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(daño);
        }
    }
}
