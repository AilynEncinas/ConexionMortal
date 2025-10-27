// DoorLaboratory.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLaboratory : MonoBehaviour
{
    public string nombreEscenaLaboratorio = "Laboratorio";
    public string mensajeSinLlave = "La puerta está cerrada. Necesitas la llave.";
    public float mensajeDuration = 2f;
    public MensajeUI mensajeUI;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryOpenDoor();
        }
    }

    private void TryOpenDoor()
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.tieneLlave)
        {
            // Opcional: reproducir sonido o efecto aquí
            SceneManager.LoadScene(nombreEscenaLaboratorio);
        }
        else
        {
            mensajeUI.MostrarMensaje("Encuentra la llave del laboratorio", 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
