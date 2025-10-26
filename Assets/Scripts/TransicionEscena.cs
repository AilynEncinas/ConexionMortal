using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TransicionEscena : MonoBehaviour
{
    public Image fadeImage;
    public float duracion = 1f;

    private void Start()
    {
        // Al iniciar la escena, hacer fade in (de negro a visible)
        StartCoroutine(FadeIn());
    }

    public void CambiarEscena(string nombreEscena)
    {
        StartCoroutine(FadeOut(nombreEscena));
    }

    private IEnumerator FadeIn()
    {
        Color c = fadeImage.color;
        for (float t = duracion; t >= 0; t -= Time.deltaTime)
        {
            c.a = t / duracion;
            fadeImage.color = c;
            yield return null;
        }
        c.a = 0;
        fadeImage.color = c;
    }

    private IEnumerator FadeOut(string nombreEscena)
    {
        Color c = fadeImage.color;
        for (float t = 0; t <= duracion; t += Time.deltaTime)
        {
            c.a = t / duracion;
            fadeImage.color = c;
            yield return null;
        }
        SceneManager.LoadScene(nombreEscena);
    }
}
