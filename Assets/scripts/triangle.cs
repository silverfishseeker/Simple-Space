using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Triangle : MonoBehaviour
{
    public GameObject bala;
    public TextMeshProUGUI vidaDisplayer;

    public float leftLimit;
    public float rightLimit;
    public float cadencia => 3 / (Engine.en.poder+1);
    public float salud;
    public float invulnerableTime;
    public Color invulnerableColor;
    
    private float nextShootTime;

    private SpriteRenderer spriteRenderer;
    private Color originalColor; 
    private bool isInvulnerable = false;

    // Start is called before the first frame update
    void Start() {
        nextShootTime = Time.time + cadencia;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        ActualizarDisplayer();
    }

    // Update is called once per frame
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
    }

    private void OnTriggerEnter2D(Collider2D colision) {
        IDangerous danger = colision.gameObject.GetComponent<IDangerous>();
        
        if (danger != null && !isInvulnerable){
            StartCoroutine(InvulnerableCooldown());
            CambiarVida(-danger.danger);
            if (salud <= 0){
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
