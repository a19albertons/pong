using System.Collections;
using UnityEngine;

public class PelotaController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float force;
    [SerializeField] float delay;
    [SerializeField] GameManager gameManager;

    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;

    // Declaramos dos constantes con las posiciones y máximas y mínimas.
    const float MAX_Y = 2.5f;
    const float MIN_Y = -2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int direccionX = Random.Range(0, 2) == 0 ? -1 : 1; // El límite superior es exclusivo (el 2 quedaría fuera).
        StartCoroutine(LanzarPelota(direccionX));
    }

    IEnumerator LanzarPelota(int direccionX)
    {
        // Definimos el ángulo en radianes usando Range, especificando el mínimo y máximo.
        float angulo = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angulo) * direccionX;


        // Determinamos si nos movemos hacia la derecha o izquierda.
        // Si el valor devuelto es 0, la dirección en Y será negativa; si es 1, será positiva.
        int direccionY = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Mathf.Sin(angulo) * direccionY;

        float posY = Random.Range(MIN_Y, MAX_Y);
        transform.position = new Vector3(0, posY, 0);

        Vector2 impulso = new Vector2(x, y);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(impulso * force, ForceMode2D.Impulse);
        // Calculamos la posición vertical de forma aleatoria.


        yield return new WaitForSeconds(delay);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gol en " + other.tag + "!!");

        if (other.tag == "PorteriaIzquierda")
        {
            // Lanzaremos la pelota hacia la derecha
            gameManager.AddPointP1();
            StartCoroutine(LanzarPelota(1));
        }
        else if (other.tag == "PorteriaDerecha")
        {
            // Lanzaremos la pelota hacia la izquierda
            gameManager.AddPointP2();
            StartCoroutine(LanzarPelota(-1));
        }
    }
}
