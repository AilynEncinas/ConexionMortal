using UnityEngine;

public class CamaraSeguimientoSuave : MonoBehaviour
{
    public Transform jugador;        // Tu personaje
    public float suavizado = 0.15f;  // Qu� tan fluida es la c�mara
    public Vector2 offset;           // Peque�o desplazamiento (opcional)
    public Vector2 minPos, maxPos;   // L�mites del �rea

    private Vector3 velocidad = Vector3.zero;

    void LateUpdate()
    {
        if (jugador == null) return;

        Vector3 destino = new Vector3(
            jugador.position.x + offset.x,
            jugador.position.y + offset.y,
            transform.position.z
        );

        // Limita el movimiento de c�mara
        destino.x = Mathf.Clamp(destino.x, minPos.x, maxPos.x);
        destino.y = Mathf.Clamp(destino.y, minPos.y, maxPos.y);

        // Movimiento suave
        transform.position = Vector3.SmoothDamp(transform.position, destino, ref velocidad, suavizado);
    }
}
