using UnityEngine;

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

    void Update()
    {
        Move();
        HandleThrow();
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
