using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deslizador : MonoBehaviour
{
    public float velocidad;
    public float limiteInferior = -5;

    // Update is called once per frame
    protected void Update()
    {
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);

        if (transform.position.y < limiteInferior)
            Destroy(gameObject);
    }
}
