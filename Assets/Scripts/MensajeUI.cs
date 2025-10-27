using UnityEngine;
using TMPro;

public class MensajeUI : MonoBehaviour
{
    public TextMeshProUGUI textoMensaje;
    private float tiempoMostrar = 0f;

    void Update()
    {
        if (tiempoMostrar > 0)
        {
            tiempoMostrar -= Time.deltaTime;
            if (tiempoMostrar <= 0)
            {
                textoMensaje.text = "";
            }
        }
    }

    public void MostrarMensaje(string mensaje, float duracion = 2f)
    {
        textoMensaje.text = mensaje;
        tiempoMostrar = duracion;
    }
}
