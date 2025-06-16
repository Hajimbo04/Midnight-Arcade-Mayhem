using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;                         // XR bindings
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class ClosetHidingLockMove : MonoBehaviour
{
    [Header("XR Rig & Camera")]
    public GameObject xrRig;
    public Transform xrCamera;

    [Header("Input")]
    public InputActionReference hideAction;               // ← drag “HideToggle” here

    [Header("XR Device Simulator (optional)")]
    public XRDeviceSimulator simulator;

    [Header("Locomotion Scripts To Disable")]
    public MonoBehaviour[] locomotionScripts;

    [Header("Hide Offset (metres, local to this trigger)")]
    public Vector3 localHideOffset = new Vector3(0f, 1.6f, -0.4f);

    bool playerInside;
    bool isHiding;
    Vector3 savedRigPos;

    // ─────────────────────────────────────────────────────────────
    void OnEnable()  { if (hideAction) hideAction.action.Enable(); }
    void OnDisable() { if (hideAction) hideAction.action.Disable(); }

    void Update()
    {
        if (!playerInside) return;

        bool keyboardN = Keyboard.current?.nKey.wasPressedThisFrame ?? false;
        bool vrButton  = hideAction && hideAction.action.WasPressedThisFrame();

        if (keyboardN || vrButton)
            ToggleHide();
    }
    // ─────────────────────────────────────────────────────────────
    void ToggleHide()
    {
        if (!isHiding)
        {
            // ENTER
            savedRigPos = xrRig.transform.position;

            Vector3 headTarget = transform.TransformPoint(localHideOffset);
            Vector3 camOffset  = xrCamera.position - xrRig.transform.position;
            xrRig.transform.position = headTarget - camOffset;

            SetLocomotion(false);
            isHiding = true;
        }
        else
        {
            // EXIT
            xrRig.transform.position = savedRigPos;

            SetLocomotion(true);
            isHiding = false;
        }
    }

    void SetLocomotion(bool enable)
    {
        foreach (var s in locomotionScripts)
            if (s) s.enabled = enable;

        if (simulator)
        {
            var x = simulator.keyboardXTranslateAction.action;
            var y = simulator.keyboardYTranslateAction.action;
            var z = simulator.keyboardZTranslateAction.action;

            if (enable) { if (!x.enabled) x.Enable(); if (!y.enabled) y.Enable(); if (!z.enabled) z.Enable(); }
            else        { if (x.enabled)  x.Disable(); if (y.enabled) y.Disable(); if (z.enabled) z.Disable(); }
        }
    }

    void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) playerInside = true; }
    void OnTriggerExit (Collider other) { if (other.CompareTag("Player")) playerInside = false; }
}
