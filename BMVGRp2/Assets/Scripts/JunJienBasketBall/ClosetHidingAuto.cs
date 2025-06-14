using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;   // XRDeviceSimulator

public class ClosetHidingLockMove : MonoBehaviour
{
    [Header("XR Rig & Camera")]
    public GameObject xrRig;
    public Transform xrCamera;

    [Header("XR Device Simulator (drop the prefab here)")]
    public XRDeviceSimulator simulator;

    [Header("Locomotion Scripts To Disable")]
    public MonoBehaviour[] locomotionScripts;   // move providers, jump, etc.

    [Header("Hide Offset (metres, local to this trigger)")]
    public Vector3 localHideOffset = new Vector3(0f, 1.6f, -0.4f);

    bool playerInside;
    bool isHiding;
    Vector3 savedRigPos;

    void Update()
    {
        if (playerInside && Keyboard.current.nKey.wasPressedThisFrame)
            ToggleHide();
    }

    void ToggleHide()
    {
        if (!isHiding)
        {
            // — ENTER —
            savedRigPos = xrRig.transform.position;

            Vector3 headTarget = transform.TransformPoint(localHideOffset);
            Vector3 camOffset  = xrCamera.position - xrRig.transform.position;
            xrRig.transform.position = headTarget - camOffset;

            SetLocomotion(false);
            isHiding = true;
        }
        else
        {
            // — EXIT —
            xrRig.transform.position = savedRigPos;

            SetLocomotion(true);
            isHiding = false;
        }
    }

    void SetLocomotion(bool enable)
    {
        // Your standard move providers, etc.
        foreach (var s in locomotionScripts)
            if (s) s.enabled = enable;

        // ✹ Freeze or un‑freeze ONLY the translate keys of the simulator
        if (simulator)
        {
            var x = simulator.keyboardXTranslateAction.action;
            var y = simulator.keyboardYTranslateAction.action;
            var z = simulator.keyboardZTranslateAction.action;

            if (enable)
            {
                if (!x.enabled) x.Enable();
                if (!y.enabled) y.Enable();
                if (!z.enabled) z.Enable();
            }
            else
            {
                if (x.enabled) x.Disable();
                if (y.enabled) y.Disable();
                if (z.enabled) z.Disable();
            }
        }
    }

    void OnTriggerEnter(Collider col) { if (col.CompareTag("Player")) playerInside = true; }
    void OnTriggerExit (Collider col) { if (col.CompareTag("Player")) playerInside = false; }
}
