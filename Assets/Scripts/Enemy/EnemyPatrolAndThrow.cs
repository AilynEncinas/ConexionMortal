using UnityEngine;
using System.Collections;

public class EnemyPatrolAndThrow : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2f;             // velocidad del movimiento
    public float leftLimit = -5f;        // límite izquierdo
    public float rightLimit = 5f;        // límite derecho
    private bool movingRight = true;     // dirección actual

    [Header("Ataque")]
    public GameObject projectilePrefab;  // prefab del proyectil
    public Transform throwPoint;         // punto desde donde se lanza
    public float throwInterval = 2f;     // cada cuántos segundos lanza algo
    private float throwTimer = 0f;

    [Header("Tiempo de vida")]
    public float lifeTime = 15f;         // tiempo que estará activo
    private float lifeTimer = 0f;
    private bool isLeaving = false;      // para evitar iniciar varias veces la corrutina

    void Update()
    {
        if (!isLeaving)
        {
            Move();
            HandleThrow();
            HandleLifeTime();
        }
    }

    void Move()
    {
        // Moverse en la dirección actual
        if (movingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Verificar límites
        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
            Flip();
        }
        else if (transform.position.x <= leftLimit)
        {
            movingRight = true;
            Flip();
        }
    }

    void HandleThrow()
    {
        throwTimer += Time.deltaTime;
        if (throwTimer >= throwInterval)
        {
            ThrowProjectile();
            throwTimer = 0f;
        }
    }

    void ThrowProjectile()
    {
        if (projectilePrefab != null && throwPoint != null)
        {
            Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);
        }
    }

    void HandleLifeTime()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime && !isLeaving)
        {
            isLeaving = true;
            StartCoroutine(LeaveAndDeactivate());
        }
    }

    private IEnumerator LeaveAndDeactivate()
    {
        float leaveSpeed = 3f; // velocidad a la que se va
        float targetX = transform.position.x + (movingRight ? 5f : -5f); // se mueve hacia un lado

        while (Mathf.Abs(transform.position.x - targetX) > 0.1f)
        {
            transform.Translate((movingRight ? Vector2.right : Vector2.left) * leaveSpeed * Time.deltaTime);
            yield return null;
        }

        gameObject.SetActive(false); // desactivar villano
    }

    void Flip()
    {
        // Voltear el sprite horizontalmente
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Para ver los límites en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftLimit, transform.position.y, 0),
                        new Vector3(rightLimit, transform.position.y, 0));
    }
}
