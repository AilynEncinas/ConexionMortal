using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private Vector3 lastCheckpointPosition;

    private Rigidbody2D rb;
    private Collider2D col;

    [Header("Offset del respawn (en unidades del mundo)")]
    public float respawnOffsetY = 0.5f; // cuanto más alto reaparece (ajustable en el Inspector)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        currentHealth = maxHealth;
        lastCheckpointPosition = transform.position; // posición inicial
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("El jugador fue golpeado. Vidas restantes: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void UpdateCheckpoint(Vector3 newPosition)
    {
        lastCheckpointPosition = newPosition;
        Debug.Log("✅ Checkpoint guardado en: " + newPosition);
    }

    private void Die()
    {
        Debug.Log("💀 El jugador ha muerto");
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        // Desactiva la física antes del respawn
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        col.enabled = false;

        // Espera un instante antes de reaparecer
        yield return new WaitForSeconds(1f);

        // Reaparece un poco arriba del checkpoint
        Vector3 safePosition = lastCheckpointPosition + new Vector3(0, respawnOffsetY, 0);
        transform.position = safePosition;

        // Reactiva la física
        rb.simulated = true;
        col.enabled = true;

        // Restaura vidas
        currentHealth = maxHealth;

        Debug.Log("✨ Jugador reapareció correctamente en: " + safePosition);
    }
}
