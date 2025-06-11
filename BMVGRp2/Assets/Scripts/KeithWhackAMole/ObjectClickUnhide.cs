using UnityEngine;
using System.Collections.Generic;

public class ObjectClickUnhide : MonoBehaviour
{
    public GameObject objectToUnhide; // Assign the object to unhide in the Inspector
    public MoleManager moleManager;   // Assign the MoleManager in the Inspector
    public List<Mole> moles;          // Assign the list of Mole scripts in the Inspector

    // This method can now be called from Unity Events
    public void UnhideObject()
    {
        if (objectToUnhide != null)
        {
            objectToUnhide.SetActive(true);

            // Enable the MoleManager and start its activation cycle
            if (moleManager != null)
            {
                moleManager.enabled = true;
                StartCoroutine(moleManager.ActivateMoles());
            }

            // Enable each Mole script, reset their positions, and restart their cycle
            foreach (var mole in moles)
            {
                if (mole != null)
                {
                    mole.enabled = true;
                    mole.transform.position = mole.InitialPosition; // Reset to initial position
                    mole.StopAllCoroutines(); // Stop any running coroutines
                    mole.StartCoroutine(mole.RiseAndFallCycle()); // Restart the cycle
                }
            }
        }
    }
}
