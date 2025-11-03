using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;

    private void Start()
    {
        // Busca al jugador por su tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
            // Calcula la dirección hacia el jugador
            Vector2 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }

        // Destruye el proyectil después de unos segundos (por seguridad)
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Si golpea al jugador, le quita vida
            collision.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            // Si toca el suelo u otro objeto, se destruye
            Destroy(gameObject);
        }
    }
}
