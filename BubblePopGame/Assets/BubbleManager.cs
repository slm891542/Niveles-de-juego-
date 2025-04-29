using UnityEngine;
using System.Collections.Generic;

public class BubbleManager : MonoBehaviour
{
    public int minMatches = 3;

    public void CheckForMatches(GameObject bubble)
    {
        List<GameObject> matchingBubbles = FindMatchingBubbles(bubble);
        if (matchingBubbles.Count >= minMatches)
        {
            foreach (GameObject b in matchingBubbles)
            {
                Destroy(b);
            }
        }
    }

    List<GameObject> FindMatchingBubbles(GameObject startBubble)
    {
        List<GameObject> matches = new List<GameObject>();
        Queue<GameObject> queue = new Queue<GameObject>();

        SpriteRenderer startRenderer = startBubble.GetComponent<SpriteRenderer>();
        if (startRenderer == null) return matches;

        string startColor = startRenderer.sprite.name;

        queue.Enqueue(startBubble);
        matches.Add(startBubble);

        while (queue.Count > 0)
        {
            GameObject current = queue.Dequeue();
            Collider2D[] neighbors = Physics2D.OverlapCircleAll(current.transform.position, 1f);

            foreach (var hit in neighbors)
            {
                GameObject neighbor = hit.gameObject;
                if (!matches.Contains(neighbor))
                {
                    SpriteRenderer neighborRenderer = neighbor.GetComponent<SpriteRenderer>();
                    if (neighborRenderer != null && neighborRenderer.sprite.name == startColor)
                    {
                        matches.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
        return matches;
    }
}
