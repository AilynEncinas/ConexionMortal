using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        InventoryManager.Instance.tieneLlave = true;
        Debug.Log("Has recogido la llave del laboratorio.");
        gameObject.SetActive(false); // desaparece la llave
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
