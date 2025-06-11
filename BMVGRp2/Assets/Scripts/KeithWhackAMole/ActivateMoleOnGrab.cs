using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ActivateMoleOnGrab : MonoBehaviour
{
    public List<Mole> moleScripts;
    public MonoBehaviour additionalScript;
    public GameObject objectToDeactivate;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Reset the score when the hammer is picked up
        if (ScoreManagerKeith.Instance != null)
        {
            ScoreManagerKeith.Instance.ResetScore();
        }

        foreach (var moleScript in moleScripts)
        {
            if (moleScript != null)
            {
                moleScript.enabled = true;
            }
        }

        if (additionalScript != null)
        {
            additionalScript.enabled = true;
        }

        TimerManager timerManager = GetComponent<TimerManager>();
        if (timerManager != null)
        {
            timerManager.StartTimer();
        }
    }
}
