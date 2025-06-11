using UnityEngine;
using System.Collections.Generic;

public class MoleCycleStarter : MonoBehaviour
{
    public List<Mole> moles; // Assign the list of Mole scripts in the inspector

    // This method can be called to start the rise and fall cycle for each mole
    public void StartMoleCycles()
    {
        foreach (var mole in moles)
        {
            if (mole != null)
            {
                mole.StartCoroutine(mole.GetType().GetMethod("RiseAndFallCycle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(mole, null) as System.Collections.IEnumerator);
            }
        }
    }
}
