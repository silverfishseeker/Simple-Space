using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Triangle : MonoBehaviour
{
    public GameObject bala;
    public GameObject explosionPrefab;
    public TextMeshProUGUI vidaDisplayer;
    public Image cooldownBar;

    public float leftLimit;
    public float rightLimit;
    public float cadencia => 3 / (Engine.en.poder+1);
    public float explosionCooldown;
    public float salud;
    public float invulnerableTime;
    public Color invulnerableColor;
    
    private float nextShootTime;
    private float nextExplosionTime = 0f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor; 
    private bool isInvulnerable = false;

    void Start() {
        nextShootTime = Time.time + cadencia;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        ActualizarDisplayer();
        cooldownBar.fillAmount = 1;
    }

    void Update()
    {
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posicionXLimitada  = Mathf.Clamp(posicionRaton.x, leftLimit, rightLimit);
        transform.position = new Vector3(posicionXLimitada , transform.position.y, transform.position.z);

        while (Engine.en.isOn && nextShootTime < Time.time){
            nextShootTime += cadencia;
            GameObject nuevoPrefab = Instantiate(bala, transform.position, Quaternion.identity);
            nextShootTime = Time.time + cadencia;
        }

        if (Engine.en.isOn && Input.GetKeyDown(KeyCode.R) && Time.time >= nextExplosionTime) {
            Engine.en.poder --;
            nextExplosionTime = Time.time + explosionCooldown;

            GameObject nuevaExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (Time.time < nextExplosionTime)
            cooldownBar.fillAmount = 1 - (nextExplosionTime - Time.time) / explosionCooldown;
        else
            cooldownBar.fillAmount = 1; 
    }

    private void OnTriggerEnter2D(Collider2D colision) {
        IDangerous danger = colision.gameObject.GetComponent<IDangerous>();
        
        if (danger != null && !isInvulnerable){
            StartCoroutine(InvulnerableCooldown());
            CambiarVida(-danger.danger);
            if (salud <= 0 && Engine.en.isOn){
                Engine.en.GameOver();
            }
        }
    }

    private IEnumerator InvulnerableCooldown() {
        isInvulnerable = true;
        spriteRenderer.color = invulnerableColor;

        yield return new WaitForSeconds(invulnerableTime);

        isInvulnerable = false;
        spriteRenderer.color = originalColor;
    }

    public void CambiarVida(float f){
        salud += f;
        ActualizarDisplayer();
    }

    private void ActualizarDisplayer() => vidaDisplayer.text = salud.ToString(); 
}
