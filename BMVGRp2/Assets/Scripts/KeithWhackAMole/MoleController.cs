using UnityEngine;

public class MoleController : MonoBehaviour
{
    public Mole moleScript; // Assign the Mole script in the inspector

    void OnEnable()
    {
        if (moleScript != null)
        {
            moleScript.enabled = true; // Ensure the Mole script is active
        }
    }

    void OnDisable()
    {
        if (moleScript != null)
        {
            moleScript.StopAllCoroutines(); // Stop all coroutines in the Mole script
            moleScript.transform.position = moleScript.InitialPosition; // Reset position using the public property
        }
    }
}
