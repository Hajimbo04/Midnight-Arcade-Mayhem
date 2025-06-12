using UnityEngine;

public class SpawnerActivator : MonoBehaviour
{
    public Canvas uiCanvas;                // Reference to UI Canvas
    public TargetSpawner targetSpawner;   // Reference to spawner
    public float activationDistance = 2f; // Distance to trigger UI
    private Transform playerCamera;

    private void Start()
    {
        playerCamera = Camera.main.transform;
        uiCanvas.enabled = false; // Hide UI at start
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerCamera.position);
        if (distance < activationDistance)
        {
            uiCanvas.enabled = true;
        }
        else
        {
            uiCanvas.enabled = false;
        }
    }

    // Called from the UI button
    public void StartSpawning()
    {
        targetSpawner.StartSpawning();
        uiCanvas.enabled = false; // Optionally hide UI after start
    }
}
