using UnityEngine;

public class TeleportAndLock : MonoBehaviour
{
    public GameObject targetObject; // The target GameObject to teleport to
    public GameObject playerObject; // The player GameObject to teleport
    private bool isPositionLocked = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Use the "E" key for teleportation
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        if (targetObject != null && playerObject != null)
        {
            // Teleport the player to the target object's position
            playerObject.transform.position = targetObject.transform.position;
            LockPlayerPosition();
        }
        else
        {
            Debug.LogWarning("Target object or player object is not assigned.");
        }
    }

    void LockPlayerPosition()
    {
        isPositionLocked = true;
    }

    void FixedUpdate()
    {
        if (isPositionLocked && playerObject != null)
        {
            // Continuously set the player's position to the target's position to lock it
            playerObject.transform.position = targetObject.transform.position;
        }
    }
}
