using UnityEngine;

/// <summary>
/// Clase que gestiona los ajustes del juego, como si el jugador 2 es humano o no.
/// </summary>
public class Settings : MonoBehaviour
{
    private bool isPlayer2Human;

    public static Settings instance;

    /// <summary>
    /// Al ser levantada la instancia de la clase comprueba si el jugador 2 es humano o no y lo guarda en la variable isPlayer2Human
    /// </summary>
    void Start()
    {
        // Deshabilitado en caso de que no exista la clave y comprobamos esta variable al arrancar el juego para saber si el jugador 2 es humano o no.
        isPlayer2Human = PlayerPrefs.GetString("isPlayer2Human", "false") == "true";
    }

    /// <summary>
    /// Devuelve si el jugador 2 es humano o no.
    /// </summary>
    /// <returns>Devuelve si el jugador 2 es humano o no</returns>
    public bool GetIsPlayer2Human()
    {
        // Devuelve si el jugador 2 es humano o no.
        return isPlayer2Human;
    }

    /// <summary>
    /// Patron singleton para asegurar una unica instancia de los ajustes de usuario en todo el ciclo de vida del juego
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeIsPlayer2Human()
    {
        // Valor a obtener el que exista en PlayerPrefs. Al hacer esto ya se espera un valor preestablecido
        string isPlayer2HumanActual = PlayerPrefs.GetString("isPlayer2Human", "false"); 

        // Si el valor es true se guarda como false
        if (isPlayer2HumanActual == "true")
        {
            PlayerPrefs.SetString("isPlayer2Human", "false");
            isPlayer2Human = false;
        }
        else
        {
            PlayerPrefs.SetString("isPlayer2Human", "true");
            isPlayer2Human = true;
        }
    }
}