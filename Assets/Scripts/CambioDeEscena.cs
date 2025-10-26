using UnityEngine;

public class CambioDeEscena : MonoBehaviour
{
    [SerializeField] private string nombreSiguienteEscena;
    private TransicionEscena transicion;

    private void Start()
    {
        transicion = FindObjectOfType<TransicionEscena>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transicion.CambiarEscena(nombreSiguienteEscena);
        }
    }
}
