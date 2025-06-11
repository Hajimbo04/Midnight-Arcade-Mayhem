using UnityEngine;
using TMPro; // Add this line to use TextMeshPro

public class PositionCanvasAboveObject : MonoBehaviour
{
    public GameObject targetObject; // The GameObject to attach the Canvas to
    public Canvas worldCanvas; // Reference to the existing World Canvas
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro component
    public float heightOffset = 1.0f; // Height above the GameObject
    public float xOffset = 0.0f; // X-axis offset
    public float yOffset = 0.0f; // Y-axis offset
    public float zOffset = 0.0f; // Z-axis offset

    void Start()
    {
        if (worldCanvas != null && targetObject != null)
        {
            // Set the Canvas as a child of the target object
            worldCanvas.transform.SetParent(targetObject.transform, false);

            // Adjust the Canvas's position with specified offsets
            worldCanvas.transform.localPosition = new Vector3(xOffset, heightOffset + yOffset, zOffset);

            // Optionally, adjust the Canvas's scale
            worldCanvas.transform.localScale = new Vector3(0.005f, 0.005f, 0.01f); // Adjust scale as needed
        }
        else
        {
            Debug.LogWarning("Please assign both the targetObject and worldCanvas in the Inspector.");
        }
    }
}
