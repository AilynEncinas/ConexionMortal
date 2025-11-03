using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public GameObject enemy; // El enemigo que será activado

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador entra al área
        if (other.CompareTag("Player"))
        {
            // Activa el script de patrullaje y disparo
            enemy.GetComponent<EnemyPatrolAndThrow>().enabled = true;
            Debug.Log("¡Enemigo activado!");
        }
    }
}
