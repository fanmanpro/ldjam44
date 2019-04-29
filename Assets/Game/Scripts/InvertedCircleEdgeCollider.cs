using UnityEngine;

public class InvertedCircleEdgeCollider : MonoBehaviour
{
    public int Resolution;
    public float Radius;

    void Awake()
    {
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[Resolution + 1];
        for(int i = 0; i < Resolution; i++)
        {
            float angle = 2 * Mathf.PI * i / Resolution;
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);
            points[i] = new Vector2(x, y);
        }
        points[Resolution] = points[0];
        collider.points = points;
    }
}
