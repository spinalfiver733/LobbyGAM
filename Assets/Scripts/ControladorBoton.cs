using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControladorBoton : MonoBehaviour
{
    public ControladorCamino controladorCamino;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
    }

    // Cambiamos este m�todo a p�blico
    public void OnBotonPresionado(BaseInteractionEventArgs args)
    {
        controladorCamino.CambiarEstadoCamino();
    }
}