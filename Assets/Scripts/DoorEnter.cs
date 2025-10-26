using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class DoorEnter : MonoBehaviour
{
    [Header("Configuración")]
    public string sceneToLoad = "Laboratorio";      // Nombre de la escena a cargar
    public float fadeDuration = 1f;            // Duración del fundido
    public GameObject pressUpText;             // Texto "Presiona ↑ para entrar"
    public Image fadeImage;                    // Imagen negra para el fundido

    private bool playerInRange = false;
    private bool isFading = false;

    private void Start()
    {
        if (pressUpText != null)
            pressUpText.SetActive(false);

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0; // transparente al inicio
            fadeImage.color = c;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.UpArrow) && !isFading)
        {
            StartCoroutine(FadeAndChangeScene());
        }
        if (playerInRange && Input.GetKeyDown(KeyCode.W) && !isFading)
        {
            StartCoroutine(FadeAndChangeScene());
        }
    }

    private IEnumerator FadeAndChangeScene()
    {
        isFading = true;
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration); // aumenta opacidad
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressUpText != null)
                pressUpText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressUpText != null)
                pressUpText.SetActive(false);
        }
    }
}
