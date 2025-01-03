using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControladorCamino : MonoBehaviour
{
    public Material materialNormal;
    public Material materialEmisivo;
    private Renderer rendererCamino;
    private bool estaEncendido = false;

    void Start()
    {
        rendererCamino = GetComponent<Renderer>();
        rendererCamino.material = materialNormal;
    }

    public void CambiarEstadoCamino()
    {
        estaEncendido = !estaEncendido;

        if (estaEncendido)
        {
            rendererCamino.material = materialEmisivo;
        }
        else
        {
            rendererCamino.material = materialNormal;
        }
    }

    public void OnTriggerPressed()
    {
        CambiarEstadoCamino();
    }
}