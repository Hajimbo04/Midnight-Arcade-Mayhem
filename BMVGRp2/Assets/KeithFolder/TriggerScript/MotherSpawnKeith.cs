using UnityEngine;

public class MotherSpawnKeith : MonoBehaviour
{
    public GameObject objectToHide; // The object you want to hide
    public GameObject objectToMove; // The object you want to move to the hidden object's location
    public GameObject objectToUnhide; // The object you want to unhide
    public Animator motherAnimator; // Animator for the object with MotherSlap1 and MotherSlap2
    public Animator playerAnimator; // Animator for the object with PlayerRecieveSlap

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger is activated by the player or a specific object
        if (other.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            // Hide the original object
            if (objectToHide != null)
            {
                objectToHide.SetActive(false);
            }

            // Move the specified object to the position and rotation of the hidden object
            if (objectToMove != null && objectToHide != null)
            {
                objectToMove.transform.position = objectToHide.transform.position;
                objectToMove.transform.rotation = objectToHide.transform.rotation;
            }

            // Unhide the specified object
            if (objectToUnhide != null)
            {
                objectToUnhide.SetActive(true);
            }

            // Play animations
            if (motherAnimator != null)
            {
                motherAnimator.Play("MotherSlap1");
            }

            if (playerAnimator != null)
            {
                playerAnimator.Play("PlayerReceiveSlap");
            }
        }
    }
}
