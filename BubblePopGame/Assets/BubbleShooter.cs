using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    public GameObject[] bubblePrefabs;
    public Transform shootPoint;
    public float shootSpeed = 10f;
    public BubbleManager bubbleManager;

    private GameObject currentBubble;

    void Start()
    {
        SpawnNewBubble();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentBubble != null)
        {
            ShootBubble();
        }
    }

    void SpawnNewBubble()
    {
        currentBubble = Instantiate(bubblePrefabs[Random.Range(0, bubblePrefabs.Length)], shootPoint.position, Quaternion.identity);
        var rb = currentBubble.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = true;

        var matchScript = currentBubble.GetComponent<BubbleMatch>();
        matchScript.bubbleManager = bubbleManager;
    }

    void ShootBubble()
    {
        var rb = currentBubble.GetComponent<Rigidbody2D>();
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootPoint.position).normalized;
        rb.bodyType = RigidbodyType2D.Kinematic;
        currentBubble = null;

        Invoke(nameof(SpawnNewBubble), 0.5f);
    }
}
