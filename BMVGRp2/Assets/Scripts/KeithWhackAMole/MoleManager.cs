using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoleManager : MonoBehaviour
{
    public List<Mole> moles; // List of all mole scripts
    private Queue<Mole> moleQueue = new Queue<Mole>();

    void Start()
    {
        // Shuffle the list of moles to randomize the queue
        ShuffleMoles();

        // Enqueue all moles
        foreach (var mole in moles)
        {
            moleQueue.Enqueue(mole);
        }

        // Start the activation cycle
        StartCoroutine(ActivateMoles());
    }

    private void ShuffleMoles()
    {
        for (int i = 0; i < moles.Count; i++)
        {
            Mole temp = moles[i];
            int randomIndex = Random.Range(i, moles.Count);
            moles[i] = moles[randomIndex];
            moles[randomIndex] = temp;
        }
    }

    public IEnumerator ActivateMoles() // Changed to public
    {
        while (true)
        {
            // Activate the first three moles in the queue
            for (int i = 0; i < 1 && moleQueue.Count > 0; i++)
            {
                Mole mole = moleQueue.Dequeue();
                StartCoroutine(ActivateMole(mole));
            }

            // Wait for a while before checking the queue again
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator ActivateMole(Mole mole)
    {
        // Calculate the active duration based on random up and down durations
        float upDuration = Random.Range(1.0f, 3.0f);
        float downDuration = Random.Range(2.0f, 4.0f);
        float activeDuration = upDuration + downDuration;

        // Activate the mole
        mole.enabled = true;

        // Wait for the active duration
        yield return new WaitForSeconds(activeDuration);

        // Deactivate the mole
        mole.enabled = false;

        // Re-enqueue the mole to the end of the queue
        moleQueue.Enqueue(mole);
    }
}
