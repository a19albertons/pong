using UnityEngine;

public class PalaController : MonoBehaviour
{
    const float MAX_Y = 4.2f;
    const float MIN_Y = -4.2f;
    [SerializeField] float speed = 10f;

    [Tooltip("Aqui se pone la pelota del juego")]
    [SerializeField] GameObject pelota;

    // Mejoras a la IA rival, para que no sea tan perfecta y se pueda ganar.
    [Header("Ajustes de la IA (solo pala 2)")]
    [SerializeField] float errorMargin = 0.5f; // Margen de error para la IA
    [SerializeField] float reactionTime = 0.2f; // Tiempo de reacción de la IA
    [SerializeField] float tolerance = 0.1f; // Tolerancia para el movimiento de la IA

    Rigidbody2D rb;
    float objectivoY; // Posición Y objetivo de la pala
    float tiempoUltimoCalculo = 0f; // Tiempo desde el último cálculo de la posición de la pelota

    /// <summary>
    /// Inicializa la clase cogiendo varios valores base
    /// </summary>
    void Start()
    {
        rb = pelota.GetComponent<Rigidbody2D>();
        objectivoY = transform.position.y;
    }

    /// <summary>
    /// Actualiza la posición del jugador en función del tag y si es el 2 en función de si es humano o no.
    /// </summary>
    void Update()
    {
        // Se puede añadir un nuevo comprobante donde se mire el estado de la pelota (activa o desactiva) 
        // para evitar el movimiento antes de que empiece el juego o alternativamente cuando acaba
        if (gameObject.CompareTag("Pala2"))
        {
            if (Settings.instance.getIsPlayer2Human())
            {
                // Si es humano
                if (Input.GetKey("up") && transform.position.y < MAX_Y)
                {
                    // Movimiento hacia arriba
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                }
                if (Input.GetKey("down") && transform.position.y > MIN_Y)
                {
                    // Movimiento hacia abajo
                    transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
                }
            }
            else
            {
                LogicaIA();
            }

        }
        // Si es el jugador de la pala 1, se mueve con las teclas W y S exclusivamente.
        else if (gameObject.CompareTag("Pala1"))
        {
            if (Input.GetKey("w") && transform.position.y < MAX_Y)
            {
                // Movimiento hacia arriba
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey("s") && transform.position.y > MIN_Y)
            {
                // Movimiento hacia abajo
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
        }
    }

    /// <summary>
    /// Lógica especifica que se gestiona para la IA del jugador 2
    /// </summary>
    private void LogicaIA()
    {
        // Mira el tiempo desde el último movimiento
        tiempoUltimoCalculo += Time.deltaTime;
        if (tiempoUltimoCalculo >= reactionTime)
        {
            // Calcula un nuevo objetivo con un margen de error y reinicia el tiempo desde el último cálculo
            float error = Random.Range(-errorMargin, errorMargin);
            objectivoY = rb.position.y + error;
            tiempoUltimoCalculo = 0f;
        }

        // Calcula la distancia actual
        float distancia = objectivoY - transform.position.y;

        // Aplica el margen de tolerarncia para que la pala no se mueva constantemente
        if (Mathf.Abs(distancia) > tolerance)
        {
            float newY = Mathf.MoveTowards(transform.position.y, objectivoY, speed * Time.deltaTime);

            // Bordes de la pantalla
            newY = Mathf.Clamp(newY, MIN_Y, MAX_Y);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
