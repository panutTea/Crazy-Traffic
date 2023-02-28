using UnityEngine;

public class PointerController : MonoBehaviour
{
    public float pointerLength = 5f;
    public float pointerWidth = 0.02f;
    public Color pointerColor = Color.white;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = pointerWidth;
        lineRenderer.startColor = pointerColor;
        lineRenderer.endColor = pointerColor;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Set the start position of the line renderer to the position of the camera
        lineRenderer.SetPosition(0, transform.position);

        // Cast a ray from the camera forward
        Ray ray = new Ray(transform.position, transform.forward);

        // Check if the ray hits anything
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pointerLength))
        {
            // If the ray hits something, set the end position of the line renderer to the point of the hit
            lineRenderer.SetPosition(1, hit.point);

        }
        else
        {
            // If the ray doesn't hit anything, set the end position of the line renderer to the end of the pointer length
            lineRenderer.SetPosition(1, transform.position + transform.forward * pointerLength);
        }
    }
}
