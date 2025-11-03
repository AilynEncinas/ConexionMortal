using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.UpdateCheckpoint(transform.position);
                Debug.Log("Nuevo checkpoint guardado en: " + transform.position);
            }
        }
    }
}
