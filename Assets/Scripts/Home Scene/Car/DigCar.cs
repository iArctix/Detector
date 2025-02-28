using UnityEngine;

public class DigCar : MonoBehaviour
{

    private bool playerInRange = false;
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.ReturnHome();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}