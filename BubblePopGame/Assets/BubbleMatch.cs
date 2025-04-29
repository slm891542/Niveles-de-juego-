using UnityEngine;

public class BubbleMatch : MonoBehaviour
{
    private Rigidbody2D rb;
    public BubbleManager bubbleManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (bubbleManager == null)
        {
            bubbleManager = FindFirstObjectByType<BubbleManager>();
            if (bubbleManager == null)
                Debug.LogError("No BubbleManager found for " + gameObject.name);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            StopBubble();
            transform.SetParent(bubbleManager.transform);
            bubbleManager.CheckForMatches(gameObject);
        }
    }

    void StopBubble()
    {
        rb.simulated = false; 
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

