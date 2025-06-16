using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MotherSpawnKeith : MonoBehaviour
{
    public GameObject objectToHide; // The object you want to hide
    public GameObject objectToMove; // The object you want to move to the hidden object's location
    public GameObject objectToUnhide; // The object you want to unhide
    public Animator motherAnimator; // Animator for the object with MotherSlap1 and MotherSlap2
    public Animator playerAnimator; // Animator for the object with PlayerReceiveSlap
    public string nextSceneName; // The name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToHide != null)
            {
                objectToHide.SetActive(false);
            }

            if (objectToMove != null && objectToHide != null)
            {
                objectToMove.transform.position = objectToHide.transform.position;
                objectToMove.transform.rotation = objectToHide.transform.rotation;
            }

            if (objectToUnhide != null)
            {
                objectToUnhide.SetActive(true);
            }

            if (motherAnimator != null)
            {
                motherAnimator.Play("MotherSlap1");
            }

            if (playerAnimator != null)
            {
                playerAnimator.Play("PlayerReceiveSlap");
            }

            // Start the coroutine to load the scene after a delay
            StartCoroutine(LoadSceneAfterDelay(3f));
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("3 seconds passed, attempting to load scene: " + nextSceneName);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set.");
        }
    }
}
