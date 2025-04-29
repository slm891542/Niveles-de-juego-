using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AimTrajectory : MonoBehaviour
{
    public int maxReflections = 5;
    public float maxLength = 20f;
    public LayerMask reflectMask;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawTrajectory(Vector2 startPos, Vector2 direction)
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPos);

        Vector2 currentPosition = startPos;
        Vector2 currentDirection = direction.normalized;

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentDirection, maxLength, reflectMask);

            if (hit.collider != null)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(i + 1, hit.point);

                currentPosition = hit.point;
                currentDirection = Vector2.Reflect(currentDirection, hit.normal);
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(i + 1, currentPosition + currentDirection * maxLength);
                break;
            }
        }
    }

    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}
