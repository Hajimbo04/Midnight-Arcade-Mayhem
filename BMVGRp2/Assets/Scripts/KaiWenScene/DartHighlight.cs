using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DartHoverFeedback : MonoBehaviour
{
    public Material defaultMaterial;
    public Material highlightMaterial;

    private Renderer dartRenderer;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;

    void Awake()
    {
        dartRenderer = GetComponent<Renderer>();
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        dartRenderer.material = highlightMaterial;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        dartRenderer.material = defaultMaterial;
    }
}
