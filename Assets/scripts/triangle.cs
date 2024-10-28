using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Triangle : MonoBehaviour
{
    /**
     * 0 -> Normal
     * 1 -> Spread
     * 2 -> Big
     */
    public int currMode;

    public GameObject bala => currMode switch {
        1 => smallBala,
        2 => bigBala,
        _ => normalBala
    };
    public GameObject normalBala;
    public GameObject smallBala;
    public GameObject bigBala;
    public GameObject explosionPrefab;
    public TextMeshProUGUI vidaDisplayer;
    public Image cooldownBar;

    public float leftLimit;
    public float rightLimit;

    public float cadenciaShortCoef;
    public float cadenciaLongCoef;
    public float cadencia {
        get {
            float coef = currMode switch {
                1 => cadenciaShortCoef,
                2 => cadenciaLongCoef,
                _ => 1
            };
            return coef * 3 / (Engine.en.poder+1);
        }
    }
    public float cadenciaRepetition;

    public float explosionCooldown;
    public float salud;
    public float invulnerableTime;
    public Color invulnerableColor;
    public float balaDamageCoef = 1;
    public int numOfNormalBala = 1;
    
    private float nextShootTime;
    private float nextRepetitionShootTime;
    private float numRepetitionBulletsLeft = 0;
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
        currMode = 0;
    }
    
    private void Shoot(){
        GameObject nuevoPrefab = Instantiate(bala, transform.position, Quaternion.identity);
        nuevoPrefab.GetComponent<Bullet>().damage*=balaDamageCoef;
    }

    void Update()
    {
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float posicionXLimitada  = Mathf.Clamp(posicionRaton.x, leftLimit, rightLimit);
        transform.position = new Vector3(posicionXLimitada , transform.position.y, transform.position.z);

        // Diparo único
        while (Engine.en.isOn && nextShootTime < Time.time){
            nextShootTime += cadencia;
            Shoot();
            numRepetitionBulletsLeft = numOfNormalBala - 1;
            nextRepetitionShootTime = Time.time + cadenciaRepetition;
        }

        // Repetición de balas normales
        if (currMode == 0) {
            while (numRepetitionBulletsLeft > 0 && nextRepetitionShootTime < Time.time) {
                nextRepetitionShootTime += cadenciaRepetition;
                numRepetitionBulletsLeft -= 1;
                Shoot();
            }
        }

        // Ataque de explosión
        if (Engine.en.isOn && Input.GetKeyDown(KeyCode.R) && Time.time >= nextExplosionTime) {
            Engine.en.poder --;
            nextExplosionTime = Time.time + explosionCooldown;

            GameObject nuevaExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        
        // Interfaz del cooldown de explosión
        if (Time.time < nextExplosionTime)
            cooldownBar.fillAmount = 1 - (nextExplosionTime - Time.time) / explosionCooldown;
        else
            cooldownBar.fillAmount = 1; 

        // Cambio de tipo de bala
        if(Input.GetKeyDown(KeyCode.E)) {
            currMode = (currMode+1)%3;
            nextShootTime = Time.time + cadencia;
        }
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
