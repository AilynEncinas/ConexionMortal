using UnityEngine;
using Cinemachine;

public class CameraZoomArea : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float newZoom = 10f;
    private float defaultZoom;

    private void Start()
    {
        defaultZoom = vcam.m_Lens.OrthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            vcam.m_Lens.OrthographicSize = newZoom;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            vcam.m_Lens.OrthographicSize = defaultZoom;
    }
}
