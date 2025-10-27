// UIManager.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Text messageText; // asume UI Text; si usas TMP, cambia el tipo

    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowMessage(string msg, float duration = 2f)
    {
        if (messageText == null) return;
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ShowRoutine(msg, duration));
    }

    private IEnumerator ShowRoutine(string msg, float duration)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
        messageText.text = "";
    }
}
