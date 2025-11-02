using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Cambia por el nombre exacto de tu escena de juego
    public string nombreEscenaJuego = "Nivel1";

    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void Opciones()
    {
        // Aquí puedes activar un panel de opciones
        Debug.Log("Abrir Opciones");
    }

    public void Creditos()
    {
        Debug.Log("Mostrar Créditos");
    }

    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
