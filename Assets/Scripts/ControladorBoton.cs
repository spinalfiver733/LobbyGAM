using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControladorBoton : MonoBehaviour
{
    public ControladorCamino controladorCamino; // Referencia al script del camino

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnBotonPresionado);
    }

    private void OnBotonPresionado(SelectEnterEventArgs args)
    {
        controladorCamino.CambiarEstadoCamino();
    }
}