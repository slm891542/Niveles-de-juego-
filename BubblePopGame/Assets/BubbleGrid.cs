using UnityEngine;

public class BubbleGrid : MonoBehaviour
{
    public int rows = 5;
    public int cols = 7;
    public float bubbleSize = 1f;
    public GameObject[] bubblePrefabs;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                SpawnBubble(r, c);
            }
        }
    }

    void SpawnBubble(int r, int c)
    {
        float startX = -(cols / 2f) * bubbleSize + bubbleSize / 2;
        float startY = (rows / 2f) * bubbleSize - bubbleSize / 2;

        Vector2 position = new Vector2(startX + c * bubbleSize, startY - r * bubbleSize);
        GameObject bubble = Instantiate(bubblePrefabs[Random.Range(0, bubblePrefabs.Length)], position, Quaternion.identity);
        bubble.transform.parent = transform;
    }
}
