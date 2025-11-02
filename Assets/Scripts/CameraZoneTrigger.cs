using UnityEngine;
using Cinemachine;

public class CameraZoneTrigger : MonoBehaviour
{
    [Tooltip("La Cinemachine Virtual Camera que queremos activar")]
    public CinemachineVirtualCamera cameraToActivate;

    [Tooltip("Priority que tomará la cámara activada (usa > 10 para que sobrepase la vcam1)")]
    public int activatePriority = 20;

    [Tooltip("Priority que volverá a tener la cámara al salir")]
    public int defaultPriority = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (cameraToActivate != null)
            cameraToActivate.Priority = activatePriority;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (cameraToActivate != null)
            cameraToActivate.Priority = defaultPriority;
    }
}
