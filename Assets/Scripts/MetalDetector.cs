using UnityEngine;

public class MetalDetector : MonoBehaviour
{
    public float detectionRange = 5f;
    public float maxDetectionDepth = 10f;
    public LayerMask itemLayer;
    public AudioSource beepSound;

    private Transform player;
    private float beepTimer = 0f;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("MetalDetector: No GameObject found with tag 'Player'! Make sure your player is tagged correctly.");
        }

        if (beepSound == null)
        {
            Debug.LogError("MetalDetector: No AudioSource assigned! Assign one in the Inspector.");
        }
    }

    void Update()
    {
        if (player != null) // Prevents null reference error
        {
            DetectClosestItem();
        }
    }

    void DetectClosestItem()
    {
        Collider[] detectedItems = Physics.OverlapSphere(player.position, detectionRange, itemLayer);

        if (detectedItems.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            Collider closestItem = null;

            foreach (Collider item in detectedItems)
            {
                float distance = Vector3.Distance(player.position, item.transform.position);
                if (item.transform.position.y < maxDetectionDepth)
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestItem = item;
                    }
                }
            }

            if (closestItem != null)
            {
                AdjustBeeping(closestDistance);
            }
        }
        else
        {
            StopBeeping();
        }
    }

    void AdjustBeeping(float distance)
    {
        if (beepSound == null) return; // Prevents errors if beepSound isn't assigned

        float beepRate = Mathf.Clamp(1f / distance, 0.2f, 2f);
        beepTimer -= Time.deltaTime;

        if (beepTimer <= 0f)
        {
            beepSound.Play();
            beepTimer = 1f / beepRate;
        }
    }

    void StopBeeping()
    {
        beepTimer = 0f;
    }
}
