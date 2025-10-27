// InventoryManager.cs
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public bool tieneLlave = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // opcional: mantener entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
