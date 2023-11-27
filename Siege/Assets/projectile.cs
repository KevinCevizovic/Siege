using System.Collections;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject ball;
    private float power = 0f;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Create a LineRenderer component for drawing the curve
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    public static Vector3 Interpolate(Vector3 start, Vector3 end, Vector3 A, Vector3 B, float t)
    {
        Vector3 a = Vector3.Lerp(start, A, t);
        Vector3 b = Vector3.Lerp(A, B, t);
        Vector3 c = Vector3.Lerp(B, end, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }

    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            power += Time.deltaTime * 10f;
            DrawCurve();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject newBall = Instantiate(ball, transform.position, Quaternion.identity);

            Vector3 start = transform.position;
            Vector3 end = transform.position + transform.forward * power;
            Vector3 A = transform.position + transform.up * 2f;
            Vector3 B = transform.position + transform.forward * power + transform.up * power/2f;
            float duration = Vector3.Distance(start, end) / power * 0.1f;
            power = 0f;

            StartCoroutine(MoveProjectile(newBall.transform, start, end, A, B, duration));
            // Clear the curve after firing
            lineRenderer.positionCount = 0;
        }
    }

    void DrawCurve()
    {
        // Draw the curve using the LineRenderer
        int numPoints = 100;
        lineRenderer.positionCount = numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = Interpolate(transform.position, transform.position + transform.forward * power,
                                         transform.position + transform.up * power,
                                         transform.position + transform.forward * power + transform.up * power, t);
            lineRenderer.SetPosition(i, point);
        }
    }

    IEnumerator MoveProjectile(Transform projectileTransform, Vector3 start, Vector3 end, Vector3 A, Vector3 B, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 position = Interpolate(start, end, A, B, t);
            projectileTransform.position = position;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the projectile reaches the end position precisely
        projectileTransform.position = end;
    }
}
